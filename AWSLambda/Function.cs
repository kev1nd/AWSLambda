using System;
using System.IO;
using System.Text;

using Newtonsoft.Json;

using Amazon.Lambda.Core;
using Amazon.Lambda.DynamoDBEvents;
using Amazon.DynamoDBv2.Model;
using Alexa.NET.Response;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace AWSLambda
{
    public class Function
    {
        private static readonly JsonSerializer _jsonSerializer = new JsonSerializer();

        //public void FunctionHandler(DynamoDBEvent dynamoEvent, ILambdaContext context)
        //{
        //    context.Logger.LogLine($"Beginning to process {dynamoEvent.Records.Count} records...");

        //    foreach (var record in dynamoEvent.Records)
        //    {
        //        context.Logger.LogLine($"Event ID: {record.EventID}");
        //        context.Logger.LogLine($"Event Name: {record.EventName}");

        //        string streamRecordJson = SerializeStreamRecord(record.Dynamodb);
        //        context.Logger.LogLine($"DynamoDB Record:");
        //        context.Logger.LogLine(streamRecordJson );
        //    }

        //    context.Logger.LogLine("Stream processing complete.");
        //}

        //private string SerializeStreamRecord(StreamRecord streamRecord)
        //{
        //    using (var writer = new StringWriter())
        //    {
        //        _jsonSerializer.Serialize(writer, streamRecord);
        //        return writer.ToString();
        //    }
        //}


        // Details here: https://github.com/timheuer/alexa-skills-dotnet


        public SkillResponse FunctionHandler(SkillRequest input, ILambdaContext context)
        {
            var requestType = input.GetRequestType();
            var speech = new Alexa.NET.Response.SsmlOutputSpeech();
            speech.Ssml = "<speak>No mataches have been found.</speak>";

 
            if (requestType == typeof(IntentRequest))
            {
                // do some intent-based stuff
                var intentRequest = input.Request as IntentRequest;
                speech.Ssml = "<speak>Intent recognised.</speak>";
                // check the name to determine what you should do
                if (intentRequest.Intent.Name.Equals("TestIntent"))
                {
                    // get the slots
                    var firstValue = intentRequest.Intent.Slots["date"].Value;
                    speech.Ssml = "<speak>Slot recognised: " + firstValue + ".</speak>";
                }
            }
            else if (requestType == typeof(Alexa.NET.Request.Type.LaunchRequest))
            {
                // default launch path executed
                speech.Ssml = "<speak>Skill launched.</speak>";
            }
            else if (requestType == typeof(AudioPlayerRequest))
            {
                // do some audio response stuff
                speech.Ssml = "<speak>Audio.</speak>";
            }

            // build the speech response 
            var dt = DateTime.Now;

            // create the response using the ResponseBuilder
            var finalResponse = ResponseBuilder.Tell(speech);
            return finalResponse;

        }


    }
}