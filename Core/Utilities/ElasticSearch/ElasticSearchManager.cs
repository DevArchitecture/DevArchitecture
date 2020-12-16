using Core.Utilities.ElasticSearch.Models;
using Microsoft.Extensions.Configuration;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Utilities.ElasticSearch
{
    public class ElasticSearchManager : IElasticSearch
    {
        private readonly ConnectionSettings _connectionSettings;

        public ElasticSearchManager(IConfiguration Configuration)
        {

            var settings = Configuration.GetSection("ElasticSearchCong").Get<ElasticSearchCong>();
            Uri uri = new Uri(settings.ConnectionString);
            _connectionSettings = new ConnectionSettings(uri);

        }

        public Results.Result CheckIndex(string indexName)
        {
            throw new NotImplementedException();
        }

        public Results.Result CreateNewIndex(IndexModel indexModel)
        {
            var elasticClient = GetElasticClient(indexModel.IndexName);


            var response = elasticClient.Indices.Create(indexModel.IndexName, se =>
                  se.Settings(a => a.NumberOfReplicas(indexModel.NumberOfReplicas)
                              .NumberOfShards(indexModel.NumberOfShards))
                  .Aliases(x => x.Alias(indexModel.AliasName))
            );

            return new Results.Result(success: response.IsValid,
                                      message: response.IsValid ? "Success" : response.ServerError.Error.Reason);

        }

        public List<T> GetAllSearch<T>(string indexName, int from = 0, int size = 10) where T : class
        {
            var elasticClient = GetElasticClient(indexName);
            var searchResponse = elasticClient.Search<T>(s => s
                            .Index(indexName)
                            .From(from)
                            .Size(size));


            return searchResponse.Documents.ToList();
        }

        public IReadOnlyDictionary<IndexName, IndexState> GetIndexList()
        {
            var elasticClient = new ElasticClient(_connectionSettings);
            return elasticClient.Indices.Get(new GetIndexRequest(Indices.All)).Indices;
        }

        public List<T> GetSearchByField<T>(string indexName, string field, string value, int from = 0, int size = 10) where T : class
        {
            var elasticClient = GetElasticClient(indexName);
            var searchResponse = elasticClient.Search<T>(s => s
                        .AllIndices()
                        .From(from)
                        .Size(size)
                        .Query(q => q.Match(
                            m => m.Field(field)
                            .Query(value)
                            .Operator(Operator.And)
                            )
                         )
                       );

            return searchResponse.Documents.ToList();
        }

        public List<T> GetSearchBySimpleQueryString<T>(string indexName, string queryName, string query, string[] fields, int from = 0, int size = 10) where T : class
        {
            var elasticClient = GetElasticClient(indexName);
            var searchResponse = elasticClient.Search<T>(s => s
                            .Index(indexName)
                            .From(from)
                            .Size(size)
                            .MatchAll()
                            .Query(a => a.SimpleQueryString(c => c
                                      .Name(queryName)
                                      .Boost(1.1)
                                      .Fields(fields)
                                      .Query(query)
                                      .Analyzer("standard")
                                      .DefaultOperator(Operator.Or)
                                      .Flags(SimpleQueryStringFlags.And | SimpleQueryStringFlags.Near)
                                      .Lenient()
                                      .AnalyzeWildcard(false)
                                      .MinimumShouldMatch("30%")
                                      .FuzzyPrefixLength(0)
                                      .FuzzyMaxExpansions(50)
                                      .FuzzyTranspositions()
                                      .AutoGenerateSynonymsPhraseQuery(false)))

         );
            return searchResponse.Documents.ToList();
        }

        public Results.Result Insert(string indexName, object item)
        {
            var elasticClient = GetElasticClient(indexName);

            var response = elasticClient.Index(item, i => i.Index(indexName));

            return new Results.Result(success: response.IsValid,
                                     message: response.IsValid ? "Success" : response.ServerError.Error.Reason);
        }

        public Results.Result InsertMany(string indexName, object[] items)
        {
            var elasticClient = GetElasticClient(indexName);
            var response = elasticClient.Bulk(a =>
                                 a.Index(indexName)
                                 .IndexMany(items));

            return new Results.Result(success: response.IsValid,
                                     message: response.IsValid ? "Success" : response.ServerError.Error.Reason);
        }


        private void ControlIndexNameNullOrEmpty(string indexName)
        {
            if (string.IsNullOrEmpty(indexName))
            {
                throw new ArgumentNullException(indexName, "Index name cannot be null or empty ");
            }
        }


        private ElasticClient GetElasticClient(string indexName)
        {
            ControlIndexNameNullOrEmpty(indexName);
            return new ElasticClient(_connectionSettings);
        }
    }

}
