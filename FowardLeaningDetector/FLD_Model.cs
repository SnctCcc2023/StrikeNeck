using Microsoft.ML.Data;
using Microsoft.ML.Trainers.FastTree;
using Microsoft.ML;

namespace strikeneck.Imaging
{
    public partial class FLD1
    {
        /// <summary>
        /// Retrains model using the pipeline generated as part of the training process. For more information on how to load data, see aka.ms/loaddata.
        /// </summary>
        /// <param name="mlContext"></param>
        /// <param name="trainData"></param>
        /// <returns></returns>
        public static ITransformer RetrainPipeline(MLContext mlContext, IDataView trainData)
        {
            var pipeline = BuildPipeline(mlContext);
            var model = pipeline.Fit(trainData);

            return model;
        }

        /// <summary>
        /// build the pipeline that is used from model builder. Use this function to retrain model.
        /// </summary>
        /// <param name="mlContext"></param>
        /// <returns></returns>
        public static IEstimator<ITransformer> BuildPipeline(MLContext mlContext)
        {
            // Data process configuration with pipeline data transformations
            var pipeline = mlContext.Transforms.ReplaceMissingValues(new[] { new InputOutputColumnPair(@"N_LEar_X", @"N_LEar_X"), new InputOutputColumnPair(@"N_LEar_Y", @"N_LEar_Y"), new InputOutputColumnPair(@"REar_REye_X", @"REar_REye_X"), new InputOutputColumnPair(@"REar_REye_Y", @"REar_REye_Y"), new InputOutputColumnPair(@"LEye_LEar_X", @"LEye_LEar_X"), new InputOutputColumnPair(@"LEye_LEar_Y", @"LEye_LEar_Y"), new InputOutputColumnPair(@"REye_N_X", @"REye_N_X"), new InputOutputColumnPair(@"REye_N_Y", @"REye_N_Y"), new InputOutputColumnPair(@"N_LEye_X", @"N_LEye_X"), new InputOutputColumnPair(@"N_+Eye_Y", @"N_+Eye_Y") })
                                    .Append(mlContext.Transforms.Concatenate(@"Features", new[] { @"N_LEar_X", @"N_LEar_Y", @"REar_REye_X", @"REar_REye_Y", @"LEye_LEar_X", @"LEye_LEar_Y", @"REye_N_X", @"REye_N_Y", @"N_LEye_X", @"N_+Eye_Y" }))
                                    .Append(mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: @"FLD", inputColumnName: @"FLD"))
                                    .Append(mlContext.MulticlassClassification.Trainers.OneVersusAll(binaryEstimator: mlContext.BinaryClassification.Trainers.FastTree(new FastTreeBinaryTrainer.Options() { NumberOfLeaves = 4, MinimumExampleCountPerLeaf = 20, NumberOfTrees = 4, MaximumBinCountPerFeature = 255, FeatureFraction = 1, LearningRate = 0.1, LabelColumnName = @"FLD", FeatureColumnName = @"Features" }), labelColumnName: @"FLD"))
                                    .Append(mlContext.Transforms.Conversion.MapKeyToValue(outputColumnName: @"PredictedLabel", inputColumnName: @"PredictedLabel"));

            return pipeline;
        }
    }

    public partial class FLD1
    {
        /// <summary>
        /// model input class for FLD1.
        /// </summary>
        #region model input class
        public class ModelInput
        {
            [LoadColumn(0)]
            [ColumnName(@"N_LEar_X")]
            public float N_LEar_X { get; set; }

            [LoadColumn(1)]
            [ColumnName(@"N_LEar_Y")]
            public float N_LEar_Y { get; set; }

            
            [LoadColumn(2)]
            [ColumnName(@"REar_REye_X")]
            public float REar_REye_X { get; set; }

            [LoadColumn(3)]
            [ColumnName(@"REar_REye_Y")]
            public float REar_REye_Y { get; set; }

            
            [LoadColumn(4)]
            [ColumnName(@"LEye_LEar_X")]
            public float LEye_LEar_X { get; set; }

            
            [LoadColumn(5)]
            [ColumnName(@"LEye_LEar_Y")]
            public float LEye_LEar_Y { get; set; }

            
            [LoadColumn(6)]
            [ColumnName(@"REye_N_X")]
            public float REye_N_X { get; set; }

            
            [LoadColumn(7)]
            [ColumnName(@"REye_N_Y")]
            public float REye_N_Y { get; set; }

            
            [LoadColumn(8)]
            [ColumnName(@"N_LEye_X")]
            public float N_LEye_X { get; set; }

            
            [LoadColumn(9)]
            [ColumnName(@"N_+Eye_Y")]
            public float N__Eye_Y { get; set; }

            
            [LoadColumn(10)]
            [ColumnName(@"FLD")]
            public string FLD { get; set; }

        }

        #endregion

        /// <summary>
        /// model output class for FLD1.
        /// </summary>
        #region model output class
        public class ModelOutput
        {
            [ColumnName(@"N_LEar_X")]
            public float N_LEar_X { get; set; }

            [ColumnName(@"N_LEar_Y")]
            public float N_LEar_Y { get; set; }

            [ColumnName(@"REar_REye_X")]
            public float REar_REye_X { get; set; }

            [ColumnName(@"REar_REye_Y")]
            public float REar_REye_Y { get; set; }

            [ColumnName(@"LEye_LEar_X")]
            public float LEye_LEar_X { get; set; }

            [ColumnName(@"LEye_LEar_Y")]
            public float LEye_LEar_Y { get; set; }

            [ColumnName(@"REye_N_X")]
            public float REye_N_X { get; set; }

            [ColumnName(@"REye_N_Y")]
            public float REye_N_Y { get; set; }

            [ColumnName(@"N_LEye_X")]
            public float N_LEye_X { get; set; }

            [ColumnName(@"N_+Eye_Y")]
            public float N__Eye_Y { get; set; }

            [ColumnName(@"FLD")]
            public uint FLD { get; set; }

            [ColumnName(@"Features")]
            public float[] Features { get; set; }

            [ColumnName(@"PredictedLabel")]
            public string PredictedLabel { get; set; }

            [ColumnName(@"Score")]
            public float[] Score { get; set; }

        }

        #endregion

        private static string MLNetModelPath = Path.GetFullPath("FLD1.zip");

        public static readonly Lazy<PredictionEngine<ModelInput, ModelOutput>> PredictEngine = new Lazy<PredictionEngine<ModelInput, ModelOutput>>(() => CreatePredictEngine(), true);

        /// <summary>
        /// Use this method to predict on <see cref="ModelInput"/>.
        /// </summary>
        /// <param name="input">model input.</param>
        /// <returns><seealso cref=" ModelOutput"/></returns>
        public static ModelOutput Predict(ModelInput input)
        {
            var predEngine = PredictEngine.Value;
            return predEngine.Predict(input);
        }

        private static PredictionEngine<ModelInput, ModelOutput> CreatePredictEngine()
        {
            var mlContext = new MLContext();
            ITransformer mlModel = mlContext.Model.Load(MLNetModelPath, out var _);
            return mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);
        }
    }
}
