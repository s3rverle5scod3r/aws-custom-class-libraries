using nuget_class_library.class_library.data.enums;

namespace nuget_class_library.class_library.aws.sns
{
    public static class EngineMappingHelper
    {
        public static string GetEngineTopicARN(Engine engine, RuntimeEnvironment runtimeEnvironment)
        {
            switch (engine)
            {
                case Engine.A:
                    return $"{runtimeEnvironment.ToString()}_SNS_SUBMISSION_TOPIC_A";
                case Engine.B:
                    return $"{runtimeEnvironment.ToString()}_SNS_SUBMISSION_TOPIC_B";
                case Engine.C:
                    return $"{runtimeEnvironment.ToString()}_SNS_SUBMISSION_TOPIC_C";
                default:
                    throw new ArgumentException("Unknown engine type requested.");         
            }
        }
    }
}

