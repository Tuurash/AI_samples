using Azure;
using Azure.AI.Inference;

using System.Text.Json;

string _githubtoken = $"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";

//Basic

#region Basic

//Working 

//var endpoint = new Uri("https://models.inference.ai.azure.com");
//var credential = new AzureKeyCredential(_githubtoken);
//var model = "gpt-4o";

//var client = new ChatCompletionsClient(
//	 endpoint,
//	 credential,
//	 new ChatCompletionsClientOptions());


//string systemPrompt = @"You are a helpful assistant.";

//string UserPrompt = Console.ReadLine() ?? "Hello";

//var requestOptions = new ChatCompletionsOptions()
//{
//	Messages =
//	 {
//		  new ChatRequestSystemMessage(""),
//		  new ChatRequestUserMessage(UserPrompt),
//	 },
//	Model = model,
//	Temperature = 1,
//	MaxTokens = 1000,

//};

//Response<ChatCompletions> response = client.Complete(requestOptions);
//System.Console.WriteLine(response.Value.Choices[0].Message.Content);


#endregion


//Identity and Invoke
//demonstrates use of functions in chat completions	and tools

#region identity_invoke

//var endpoint = new Uri("https://models.inference.ai.azure.com");

//var credential = new AzureKeyCredential(_githubtoken);

//var model = "gpt-4o";

//var functionDefinition = new FunctionDefinition("getFlightInfo")
//{
//Description = "Returns information about the next flight between two cities." +
//						"This includes the name of the airline, flight number and the date and time" +
//						"of the next flight",
//Parameters = BinaryData.FromObjectAsJson(new
//{
//Type = "object",
//Properties = new
//{
//originCity = new
//{
//Type = "string",
//Description = "The name of the city where the flight originates",
//},
//destinationCity = new
//{
//Type = "string",
//Description = "The flight destination city",
//}
//}
//},
//	 new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase })
//};
//ChatCompletionsFunctionToolDefinition tool = new ChatCompletionsFunctionToolDefinition(functionDefinition);

//var client = new ChatCompletionsClient(
//	 endpoint,
//	 credential,
//	 new ChatCompletionsClientOptions());

//var requestOptions = new ChatCompletionsOptions()
//{
//Messages =
//	 {
//		  new ChatRequestSystemMessage("You an assistant that helps users find flight information."),
//		  new ChatRequestUserMessage("I'm interested in going to Miami. What is the next flight there from Seattle?"),
//	 },
//Model = model,
//Tools = { tool }
//};

//Response<ChatCompletions> response = await client.CompleteAsync(requestOptions);

//if (response.GetRawResponse().IsError)
//{
//throw new Exception(response.GetRawResponse().ToString());
//}

//// We expect the model to ask for a tool call
//if (response.Value.Choices[0].FinishReason == "tool_calls")
//{
//// Append the model response to the chat history
//requestOptions.Messages.Add(new ChatRequestAssistantMessage(response.Value.Choices[0].Message));

//// We expect a single tool call
//if (response.Value.Choices[0].Message.ToolCalls.Count == 1)
//{
//var toolCall = response.Value.Choices[0].Message.ToolCalls[0] as ChatCompletionsFunctionToolCall;
//// We expect the tool to be a function call
//if (toolCall.GetType() == typeof(ChatCompletionsFunctionToolCall))
//{
//var functionArgs = JsonSerializer.Deserialize<Dictionary<string, string>>(toolCall.Arguments);
//System.Console.WriteLine(String.Format("Calling function {0}", toolCall.Arguments));
//var callableFunc = Type.GetType("AllowedFunctions").GetMethod(toolCall.Name);
//// We have to parse the parameters to ensure, they follow in the order,
//// in which function accepts them.
//var requiredParams = callableFunc.GetParameters();
//object[] parsedArgs = new object[requiredParams.Length];
//for (int i = 0; i < requiredParams.Length; i++)
//{
//parsedArgs[i] = functionArgs.GetValueOrDefault(requiredParams[i].Name, "");
//}
//var functionReturn = callableFunc.Invoke(null, parsedArgs);
//System.Console.WriteLine(String.Format("Function returned = {0}", functionReturn));

//requestOptions.Messages.Add(new ChatRequestToolMessage(
//	 toolCallId: toolCall.Id,
//	 content: functionReturn.ToString()
//));
//response = await client.CompleteAsync(requestOptions);
//System.Console.WriteLine(response.Value.Choices[0].Message.Content);
//}
//}
//}

//class AllowedFunctions
//{
//	public static string getFlightInfo(string originCity, string destinationCity)
//	{
//		if (originCity == "Seattle" && destinationCity == "Miami")
//		{
//			return JsonSerializer.Serialize(
//				 new Dictionary<string, string>
//				 {
//						  { "airline", "Delta" },
//						  { "flight_number", "DL123" },
//						  { "flight_date", "May 7th, 2024" },
//						  { "flight_time", "10:00AM" }
//				 }
//			);
//		}
//		return JsonSerializer.Serialize(
//			 new Dictionary<string, string>
//			 {
//					 { "error", "No flights found between the cities" }
//			 }
//		);
//	}
//}

#endregion

//multi-turn conversation

#region multi_turn_conversation

//var endpoint = new Uri("https://models.inference.ai.azure.com");
//var credential = new AzureKeyCredential(_githubtoken);
//var model = "gpt-4o";

//var client = new ChatCompletionsClient(
//	 endpoint,
//	 credential,
//	 new ChatCompletionsClientOptions());

//// Create an initial request to the chatbot.
//var requestOptions = new ChatCompletionsOptions()
//{
//	Messages =
//	 {
//		  new ChatRequestSystemMessage("You are a helpful assistant."),
//		  new ChatRequestUserMessage("What is the capital of France?"),
//	 },
//	Model = model
//};
//Response<ChatCompletions> response = client.Complete(requestOptions);
//System.Console.WriteLine(response.Value.Choices[0].Message.Content);
//// Append the model response to the chat history.
//requestOptions.Messages.Add(new ChatRequestAssistantMessage(response.Value.Choices[0].Message));
//// Append new user question.
//requestOptions.Messages.Add(new ChatRequestUserMessage("What about Spain?"));

//response = client.Complete(requestOptions);
//System.Console.WriteLine(response.Value.Choices[0].Message.Content);

#endregion

//Stream Example

#region stream_example

//var endpoint = new Uri("https://models.inference.ai.azure.com");
//var credential = new AzureKeyCredential(_githubtoken);
//var model = "gpt-4o";

//var client = new ChatCompletionsClient(
//    endpoint,
//    credential,
//    new ChatCompletionsClientOptions());

//var requestOptions = new ChatCompletionsOptions()
//{
//    Messages =
//    {
//        new ChatRequestSystemMessage("You are a helpful assistant."),
//        new ChatRequestUserMessage("Give me 5 good reasons why I should exercise every day."),
//    },
//    Model = model
//};

//StreamingResponse<StreamingChatCompletionsUpdate> response = await client.CompleteStreamingAsync(requestOptions);
//await foreach (StreamingChatCompletionsUpdate chatUpdate in response)
//{
//    if (!string.IsNullOrEmpty(chatUpdate.ContentUpdate))
//    {
//        System.Console.Write(chatUpdate.ContentUpdate);
//    }
//}
//System.Console.WriteLine("");

#endregion

// image input

#region image_input


var endpoint = new Uri("https://models.inference.ai.azure.com");
var credential = new AzureKeyCredential(_githubtoken);
var model = "gpt-4o";

var client = new ChatCompletionsClient(
	 endpoint,
	 credential,
	 new ChatCompletionsClientOptions());


ChatMessageContentItem[] userContent =
{
	 new ChatMessageTextContentItem("What's in this image?"),
	 new ChatMessageImageContentItem(
		  BinaryData.FromBytes(File.ReadAllBytes("sampleImage.png")), "image/jpeg", "low"
	 )
};


var requestOptions = new ChatCompletionsOptions()
{
	Messages =
	 {
		  new ChatRequestSystemMessage("You are a helpful assistant that describes images in details."),
		  new ChatRequestUserMessage(userContent),
	 },
	Model = model,
	Temperature = 1,
	MaxTokens = 1000,

};

Response<ChatCompletions> response = client.Complete(requestOptions);
System.Console.WriteLine(response.Value.Choices[0].Message.Content);


#endregion

//Going beyond rate limits : https://ai.azure.com/github/model/gpt-4o