﻿using System;
using Nest;

namespace log4net.ElasticSearch.Tests
{
    public abstract class ElasticSearchTestSetup : IDisposable
    {
        private readonly ConnectionSettings elasticSettings;
        private readonly string testIndex;

        protected readonly ElasticClient Client;

        protected ElasticSearchTestSetup()
        {
            testIndex = string.Format("{0}-{1}", "log_test", DateTime.Now.ToString("yyyy.MM.dd"));

            elasticSettings = new ConnectionSettings(new Uri("http://127.0.0.1:9200"))
                .SetDefaultIndex(testIndex);
            
            Client = new ElasticClient(elasticSettings);

            DeleteTestIndex();
        }

        public void Dispose()
        {
            DeleteTestIndex();
        }

        void DeleteTestIndex()
        {
            Client.DeleteIndex(new DeleteIndexRequest(testIndex));
        }
    }
}
