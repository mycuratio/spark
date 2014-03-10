﻿/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.github.com/furore-fhir/spark/master/LICENSE
 */

using Hl7.Fhir.Model;
using Spark.Config;
using Spark.Core;
using Spark.Service;
using Spark.Support;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Spark.Controllers
{
    [RoutePrefix("maintainance")]
    public class MaintainanceController : ApiController
    {
        FhirMaintainanceService maintainance;
        public MaintainanceController()
        {
            maintainance = Factory.GetFhirMaintainceService();
        }

        [HttpGet, Route("initialize")]
        public OperationOutcome Initialize()
        {
            try
            {
                return maintainance.Initialize();
            }
            catch (Exception e)
            {
                throw new SparkException(HttpStatusCode.InternalServerError, "Initialization failed", e);
            }
        }

        [HttpGet, Route("bintest")]
        public OperationOutcome BinTest()
        {
            IBlobStorage store = DependencyCoupler.Inject<IBlobStorage>();
            byte[] byteArray = Encoding.UTF8.GetBytes("Hello world!");
            MemoryStream stream = new MemoryStream(byteArray);
            store.Store("maintananceblob", stream);
            return new OperationOutcome().Message("Binary test completed.");
        }
    }
}
