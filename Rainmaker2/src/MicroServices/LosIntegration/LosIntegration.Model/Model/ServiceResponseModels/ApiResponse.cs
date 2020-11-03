﻿using Newtonsoft.Json;

namespace LosIntegration.Model.Model.ServiceResponseModels
{
    public class ApiResponse<T>
    {
        public string Status { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
        public string Code { get; set; }


        public override string ToString()
        {
            return JsonConvert.SerializeObject(value: this);
        }


       
    }

    public static class ApiResponseStatus
    {
        public static string Success => "Success";
        public static string NotFound => "NotFound";
        public static string Fail => "Fail";
        public static string Error => "Error";
    }
}