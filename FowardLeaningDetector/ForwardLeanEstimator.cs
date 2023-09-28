using Microsoft.ML;
using strikeneck.AppSetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static strikeneck.Imaging.FLD1;

namespace strikeneck.Imaging
{
    internal class ForwardLeanEstimator
    {
        private static string modelPath = "../../../../../../Imaging/mlModel/model.zip";

        internal static bool estimate(Dictionary<KeyPointName, KeyPoint> keyPoints)
        {

            var context = new MLContext();
            var model = context.Model.Load(modelPath, out var _);
            var predictionEngine = context.Model.CreatePredictionEngine<ModelInput, ModelOutput>(model);
            var input = new ModelInput();
            input.REar_REye_X = keyPoints[KeyPointName.RightEar].X - keyPoints[KeyPointName.RightEye].X;
            input.REar_REye_Y = keyPoints[KeyPointName.RightEar].Y - keyPoints[KeyPointName.RightEye].Y;
            input.LEye_LEar_X = keyPoints[KeyPointName.LeftEye].X - keyPoints[KeyPointName.LeftEar].X;
            input.LEye_LEar_Y = keyPoints[KeyPointName.LeftEye].Y - keyPoints[KeyPointName.LeftEar].Y;
            input.REye_N_X = keyPoints[KeyPointName.RightEye].X - keyPoints[KeyPointName.Nose].X;
            input.REye_N_Y = keyPoints[KeyPointName.RightEye].Y - keyPoints[KeyPointName.Nose].Y;
            input.N_LEye_X = keyPoints[KeyPointName.Nose].X - keyPoints[KeyPointName.LeftEye].X;
            input.N__Eye_Y = keyPoints[KeyPointName.Nose].Y - keyPoints[KeyPointName.LeftEye].Y;
            var output = predictionEngine.Predict(input);
            var trueScore = output.Score[0];
            var falseScore = output.Score[1];

            var bias = SettingAccessor.Load().detectionSensitivity.sensitivity;

            var isForwardLean = ( (trueScore + bias ) > falseScore );

            return isForwardLean;
        }
    }
}
