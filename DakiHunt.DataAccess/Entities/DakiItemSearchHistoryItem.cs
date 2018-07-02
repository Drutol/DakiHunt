using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using DakiHunt.ApiUtils;
using DakiHunt.DataAccess.Interfaces.Service.Base;
using DakiHunt.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DakiHunt.DataAccess.Entities
{
    public class DakiItemSearchHistoryEvent : IModelWithRelation
    {
        private Lazy<List<DakiItemSearchResultEntry>> _results;


        public long Id { get; set; }

        public DateTime DateTime { get; set; }
        public string ResultsJson { get; set; }

        [NotMapped]
        public Lazy<List<DakiItemSearchResultEntry>> Results =>
            _results ?? (_results = new Lazy<List<DakiItemSearchResultEntry>>(
                () => JsonConvert.DeserializeObject<List<DakiItemSearchResultEntry>>(ResultsJson)));


        public string[] New { get; set; }
        public string[] Sold { get; set; }

        /// <summary>
        ///     Checks if item state differs, if it does returns true.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool DiffWithPrevious(DakiItemSearchHistoryEvent other)
        {
            var diff = Results.Value.Diff(other.Results.Value, (x, y) => x.Identifier == y.Identifier,
                (x, y) => x.Price == y.Price && x.IsAvailable == y.IsAvailable);

            if (diff.Unmodified.Count() == other.Results.Value.Count)
                return false;

            New = diff.Added.Select(entry => entry.Identifier).ToArray();
            Sold = diff.Modified.Where(entry => !entry.IsAvailable).Select(entry => entry.Identifier).ToArray();

            return true;
        }

        public static void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DakiItemSearchHistoryEvent>()
                .Property(e => e.Results)
                .HasColumnType("jsonb");
        }
    }
}