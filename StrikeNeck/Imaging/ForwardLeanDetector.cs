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
            //本来は識別モデルで判定するが、現在は仮として常にtrueを返す
            return true;
        }

    }
}
