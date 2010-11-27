using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using System.Net;

namespace ZencoderWrapper
{
    public class ZencoderClient
    {
        //somewhat constant variables.
        public string API_BASE_URL = "https://app.zencoder.com/api/";

        //configuration:
        public string api_key { get; set; }
        public bool test { get; set; }

        protected internal string ContentType = "application/json";

        public ZencoderClient()
        {            
        }

        public ZencoderClient(string api_key)
            :this()
        {
            this.api_key = api_key;
        }

        public ZencoderClient(string api_key, bool test)
            :this(api_key)
        {          
            this.test = test;
        }

        /// <summary>
        /// Submits a new encoding job.
        /// </summary>
        /// <param name="job"></param>
        public JobResponse SubmitJob(JobRequest job)
        {
            ValidateConfiguration();

            //use restsharp to make the api call.
            RestClient client = GetClient();
            RestRequest request = new RestRequest("jobs", Method.POST);
            request.RequestFormat = DataFormat.Json;
            //add global settings to job before it serialilzed.
            job.api_key = api_key;
            job.test = test;
            //use our own serializer so we can exclude null values.
            //request.AddBody(job); 
            request.AddParameter(ContentType, Serializer.Serialize(job), ParameterType.RequestBody);
            request.OnBeforeDeserialization = resp =>
            {
                if (resp.StatusCode == HttpStatusCode.BadRequest)
                {
                    request.RootElement = "Error";
                }
            };

            RestResponse<JobResponse> response = client.Execute<JobResponse>(request);
            ValidateResponse(response, HttpStatusCode.Created);
            return response.Data;
        }

        public JobResponse SubmitJob(string input, string output_base_url, string output_filename)
        {
            return SubmitJob(new JobRequest(input, output_base_url, output_filename));
        }

        /// <summary>
        /// Request the current status of the job output file specified.
        /// </summary>
        /// <param name="OutputID"></param>
        /// <returns></returns>
        public JobOutputProgressResponse GetJobOutputProgress(int OutputID)
        {
            ValidateConfiguration();

            //use restsharp to make the api call.
            RestClient client = GetClient();
            RestRequest request = new RestRequest("outputs/" + RestSharp.Contrib.HttpUtility.UrlEncode(OutputID.ToString()) + "/progress?api_key=" + api_key, Method.GET);
            request.OnBeforeDeserialization = resp =>
            {
                if (resp.StatusCode == HttpStatusCode.BadRequest)
                {
                    request.RootElement = "Error";
                }
            };

            RestResponse<JobOutputProgressResponse> response = client.Execute<JobOutputProgressResponse>(request);
            ValidateResponse(response, HttpStatusCode.OK);
            return response.Data;
        }

        /// <summary>
        /// List all jobs.
        /// </summary>
        /// <returns></returns>
        public List<JobListingResponse> ListJobs(int? page, int? per_page)
        {
            ValidateConfiguration();

            string queryParams = "";
            if (page.HasValue)
            {
                queryParams += "&page=" + page.Value.ToString();
            }
            if (per_page.HasValue)
            {
                queryParams += "&per_page=" + per_page.Value.ToString();
            }

            //use restsharp to make the api call.
            RestClient client = GetClient();
            RestRequest request = new RestRequest("jobs?api_key=" + api_key + queryParams, Method.GET);
            request.OnBeforeDeserialization = resp =>
            {
                if (resp.StatusCode == HttpStatusCode.BadRequest)
                {
                    request.RootElement = "Error";
                }
            };

            RestResponse<List<JobListingResponse>> response = client.Execute<List<JobListingResponse>>(request);
            ValidateResponse(response, HttpStatusCode.OK);
            return response.Data;
        }

        /// <summary>
        /// List all jobs.
        /// </summary>
        /// <returns></returns>
        public List<JobListingResponse> ListJobs()
        {
            return ListJobs(null, null);
        }
        /// <summary>
        /// Request the details of the job specified.
        /// </summary>
        /// <param name="JobID"></param>
        /// <returns></returns>
        public JobListingResponse GetJob(int JobID)
        {
            ValidateConfiguration();

            //use restsharp to make the api call.
            RestClient client = GetClient();
            RestRequest request = new RestRequest("jobs/" + RestSharp.Contrib.HttpUtility.UrlEncode(JobID.ToString()) + "?api_key=" + api_key, Method.GET);
            request.OnBeforeDeserialization = resp =>
            {
                if (resp.StatusCode == HttpStatusCode.BadRequest)
                {
                    request.RootElement = "Error";
                }
            };

            RestResponse<JobListingResponse> response = client.Execute<JobListingResponse>(request);
            ValidateResponse(response, HttpStatusCode.OK);
            return response.Data;
        }

        /// <summary>
        /// Delete the job specified.
        /// </summary>
        /// <param name="JobID"></param>
        public void DeleteJob(int JobID)
        {
            ValidateConfiguration();

            //use restsharp to make the api call.
            RestClient client = GetClient();
            RestRequest request = new RestRequest("jobs/" + RestSharp.Contrib.HttpUtility.UrlEncode(JobID.ToString()) + "?api_key=" + api_key, Method.DELETE);
            request.OnBeforeDeserialization = resp =>
            {
                if (resp.StatusCode == HttpStatusCode.BadRequest)
                {
                    request.RootElement = "Error";
                }
            };

            RestResponse<object> response = client.Execute<object>(request);
            ValidateResponse(response, HttpStatusCode.OK);
        }

        /// <summary>
        /// If a job has failed processing you may request that it be attempted again.
        /// </summary>
        /// <param name="JobID"></param>
        public void ResubmitJob(int JobID)
        {
            UpdateJob(JobID, "resubmit");
        }

        /// <summary>
        /// Cancel a job that has not yet finished processing.
        /// </summary>
        /// <param name="JobID"></param>
        public void CancelJob(int JobID)
        {
            UpdateJob(JobID, "cancel");
        }

        private void UpdateJob(int JobID, string UpdateMethod)
        {
            ValidateConfiguration();

            //use restsharp to make the api call.
            RestClient client = GetClient();
            RestRequest request = new RestRequest("jobs/" + RestSharp.Contrib.HttpUtility.UrlEncode(JobID.ToString()) + "/" + UpdateMethod + "?api_key=" + api_key, Method.GET);
            request.OnBeforeDeserialization = resp =>
            {
                if (resp.StatusCode == HttpStatusCode.BadRequest)
                {
                    request.RootElement = "Error";
                }
            };

            RestResponse<object> response = client.Execute<object>(request);
            if (response.StatusCode == HttpStatusCode.Conflict)
            {
                throw new ApplicationException("You've tried to perform a '" + UpdateMethod + "' operation on a job that is in a state that does not support it."); 
            }
            ValidateResponse(response, HttpStatusCode.OK);
        }

        /// <summary>
        /// Create a new Zencoder account.
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        public CreateAccountResponse CreateAccount(CreateAccountRequest Account)
        {
            //this is the only api call that doesn't need pre-config.
            //ValidateConfiguration();

            //use restsharp to make the api call.
            RestClient client = GetClient();
            RestRequest request = new RestRequest("account", Method.POST);
            request.RequestFormat = DataFormat.Json;
            //use our own serializer so we can exclude null values.
            //request.AddBody(Account); 
            request.AddParameter(ContentType, Serializer.Serialize(Account), ParameterType.RequestBody);            
            request.OnBeforeDeserialization = resp =>
            {
                if (resp.StatusCode == HttpStatusCode.BadRequest)
                {
                    request.RootElement = "Error";
                }
            };

            RestResponse<CreateAccountResponse> response = client.Execute<CreateAccountResponse>(request);
            ValidateResponse(response, HttpStatusCode.Created);
            return response.Data;
        }

        /// <summary>
        /// Create a new Zencoder account.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public CreateAccountResponse CreateAccount(string email, string password)
        {
            return CreateAccount(new CreateAccountRequest(email, password));
        }

        /// <summary>
        /// Create a new Zencoder account.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="affiliate_code"></param>
        /// <returns></returns>
        public CreateAccountResponse CreateAccount(string email, string password, string affiliate_code)
        {
            return CreateAccount(new CreateAccountRequest(email, password, affiliate_code));
        }

        /// <summary>
        /// Returns Account details for the configured api_key.
        /// </summary>
        /// <returns></returns>
        public AccountResponse GetAccount()
        {
            ValidateConfiguration();

            //use restsharp to make the api call.
            RestClient client = GetClient();
            RestRequest request = new RestRequest("account?api_key=" + api_key, Method.GET);
            request.OnBeforeDeserialization = resp =>
            {
                if (resp.StatusCode == HttpStatusCode.BadRequest)
                {
                    request.RootElement = "Error";
                }
            };

            RestResponse<AccountResponse> response = client.Execute<AccountResponse>(request);
            ValidateResponse(response, HttpStatusCode.OK);
            return response.Data;
        }

        /// <summary>
        /// Turns IntegrationMode on or off for the configured api_key.
        /// </summary>
        /// <param name="IntegrationModeOn"></param>
        public void UpdateIntegrationMode(bool IntegrationModeOn)
        {
            ValidateConfiguration();

            string mode = "";
            if (!IntegrationModeOn)
            {
                mode = "live";
            }
            else
            {
                mode = "integration";
            }

            //use restsharp to make the api call.
            RestClient client = GetClient();
            RestRequest request = new RestRequest("account/" + mode + "?api_key=" + api_key, Method.GET);
            request.OnBeforeDeserialization = resp =>
            {
                if (resp.StatusCode == HttpStatusCode.BadRequest)
                {
                    request.RootElement = "Error";
                }
            };

            RestResponse<object> response = client.Execute<object>(request);
            ValidateResponse(response, HttpStatusCode.OK);
        }

        RestClient _client = null;
        /// <summary>
        /// Returns and if necessary creates a RestClient.
        /// </summary>
        /// <returns></returns>
        private RestClient GetClient()
        {
            if (_client == null)
            {
                _client = new RestClient(API_BASE_URL);
            }
            return _client;
        }

        private void ValidateResponse(IRestResponse Response, HttpStatusCode SuccessCode)
        {
            //make sure status code is the one we wants.
            if (Response.StatusCode != SuccessCode)
            {
                //check for specific status codes.
                if (Response.StatusCode == HttpStatusCode.PaymentRequired)
                {
                    throw new ApplicationException("This feature requires a payment plan.");
                }
                if (Response.StatusCode == HttpStatusCode.Conflict)
                {
                    throw new ApplicationException("A 'conflict' status code has been returned. This can happen when trying to cancel a failed job.");
                }
                else if (!string.IsNullOrEmpty(Response.Content) && Response.Content.Trim().Length > 0)
                {
                    //parse error messages.
                    var json = new RestSharp.Deserializers.JsonDeserializer { RootElement = "errors" };
                    var errors = json.Deserialize<List<string>>(new RestResponse { Content = Response.Content });
                    if (errors.Count > 0)
                    {
                        string error = errors.First();
                        //improve some of the error messages.
                        if (error == "invalid api_key")
                        {
                            error = "Zencoder return the error 'invalid api_key'. If you're sure you have the correct Api Key this can indicate a malformed json request.";
                        }
                        else if (errors.Count > 1)
                        {
                            for (int xx = 1; xx < errors.Count; xx++)
                            {
                                error += Environment.NewLine + errors[xx];
                            }
                        }
                        throw new ApplicationException(error);
                    }
                    else
                    {
                        throw new ApplicationException("An unknown error has occurred.");
                    }
                }
                else
                {
                    throw new ApplicationException("An unknown error has occurred.");
                }
            }
        }

        private void ValidateConfiguration()
        {
            if (string.IsNullOrEmpty(api_key))
            {
                throw new ApplicationException("'api_key' must be set before making a request");
            }
        }
    }
}
