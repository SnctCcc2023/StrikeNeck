using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using System.IO;

namespace strikeneck.Imaging
{
    //前傾姿勢判定器を表すクラス
    public class ForwardLeanDetector
    {
        private AttitudeEstimator estimator;
        
        public ForwardLeanDetector()
        {
            
        }

        //前傾姿勢かどうかを判定する
        public bool examin(FileInfo imageFile)
        {
            return true;
        }

    }
}
