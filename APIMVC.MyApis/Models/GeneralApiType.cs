﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIMVC.MyApis.Models
{
    // Kullanıcıy yapılacak dönüşler için tutuluyor bu class
    public class GeneralApiType<T> where T:class
    {
        public string Message { get; set; }
        public int ApiStatusCode { get; set; }
        public T ReturnObject { get; set; }
    }
}
