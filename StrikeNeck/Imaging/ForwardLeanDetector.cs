using Microsoft.ML;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using System.IO;
using System.Text;
using static StrikeNeck.Imaging.FLD1;

namespace StrikeNeck.Imaging
{
    //前傾姿勢判定器を表すクラス
    public class ForwardLeanDetector
    {
        //前傾姿勢かどうかを判定する
        public bool examin(FileInfo imageFile)
        {
            var keyPoints = AttitudeEsimator.estimate(imageFile);
            var isForwardLean = ForwardLeanEstimator.estimate(keyPoints);

            return isForwardLean;
        }

        public void retrain(DirectoryInfo correctPosture, DirectoryInfo forwardLeaningPosture)
        {

            const string TRAIN_PATH = "../../../../../../Imaging/mlModel/train.csv";
            const string MODEL_PATH = "../../../../../../Imaging/mlModel/model.zip";

            var sb = new StringBuilder();
            var sw = new StreamWriter(TRAIN_PATH, true);

            sb.AppendLine("N_LEar_X,N_LEar_Y,REar_REye_X,REar_REye_Y,LEye_LEar_X,LEye_LEar_Y,REye_N_X,REye_N_Y,N_LEye_X,N_+Eye_Y,FLD");

            foreach (var fileInfo in forwardLeaningPosture.GetFiles("*.jpeg"))
            {
                var keyPoints = AttitudeEsimator.estimate(fileInfo);
                sb.Append($"{keyPoints[KeyPointName.Nose].X - keyPoints[KeyPointName.LeftEye].X},{keyPoints[KeyPointName.Nose].Y - keyPoints[KeyPointName.LeftEye].Y},");
                sb.Append($"{keyPoints[KeyPointName.RightEar].X - keyPoints[KeyPointName.RightEye].X},{keyPoints[KeyPointName.RightEar].Y - keyPoints[KeyPointName.RightEye].Y},");
                sb.Append($"{keyPoints[KeyPointName.LeftEye].X - keyPoints[KeyPointName.LeftEar].X},{keyPoints[KeyPointName.LeftEye].Y - keyPoints[KeyPointName.LeftEar].Y},");
                sb.Append($"{keyPoints[KeyPointName.RightEye].X - keyPoints[KeyPointName.Nose].X},{keyPoints[KeyPointName.RightEye].Y - keyPoints[KeyPointName.Nose].Y},");
                sb.Append($"{keyPoints[KeyPointName.Nose].X - keyPoints[KeyPointName.LeftEye].X},{keyPoints[KeyPointName.Nose].Y - keyPoints[KeyPointName.LeftEye].Y},");
                sb.AppendLine("true");
            }

            foreach (var fileInfo in correctPosture.GetFiles("*.jpeg"))
            {
                var keyPoints = AttitudeEsimator.estimate(fileInfo);
                sb.Append($"{keyPoints[KeyPointName.Nose].X - keyPoints[KeyPointName.LeftEye].X},{keyPoints[KeyPointName.Nose].Y - keyPoints[KeyPointName.LeftEye].Y},");
                sb.Append($"{keyPoints[KeyPointName.RightEar].X - keyPoints[KeyPointName.RightEye].X},{keyPoints[KeyPointName.RightEar].Y - keyPoints[KeyPointName.RightEye].Y},");
                sb.Append($"{keyPoints[KeyPointName.LeftEye].X - keyPoints[KeyPointName.LeftEar].X},{keyPoints[KeyPointName.LeftEye].Y - keyPoints[KeyPointName.LeftEar].Y},");
                sb.Append($"{keyPoints[KeyPointName.RightEye].X - keyPoints[KeyPointName.Nose].X},{keyPoints[KeyPointName.RightEye].Y - keyPoints[KeyPointName.Nose].Y},");
                sb.Append($"{keyPoints[KeyPointName.Nose].X - keyPoints[KeyPointName.LeftEye].X},{keyPoints[KeyPointName.Nose].Y - keyPoints[KeyPointName.LeftEye].Y},");
                sb.AppendLine("false");
            }

            sw.Write(sb.ToString());
            sw.Flush();
            sw.Close();

            var context = new MLContext();
            var inputData = context.Data.LoadFromTextFile<ModelInput>(TRAIN_PATH, separatorChar: ',', hasHeader: true);
            var newModel = FLD1.RetrainPipeline(context, inputData);
            context.Model.Save(newModel, inputData.Schema, MODEL_PATH);
        }

    }
}
