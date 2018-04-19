using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Laboratory.AutoMapper
{
    public class Runner : IRunner
    {
        public string Name
        {
            get { return "AutoMapper"; }
        }

        public Runner()
        {
            #region register mapping on startup
            {
                Mappings.Register();
            }
            #endregion
        }

        public void Run()
        {
            var source = new Source
            {
                Id = 5,
                Title = "01/01/2000",
                Tags = new List<int>() { 1, 2, 3, 4, 5 },
                SourceItems = new List<SourceItem>()
                {
                    new SourceItem(){
                        ItemId = 1,
                        Title = "Item1"
                    },
                    new SourceItem(){
                        ItemId = 2,
                        Title = "Item2"
                    }
                }
            };

            var dest = Mapper.Map<Source, SourceDto>(source);
            Console.WriteLine("source to dest:");
            Console.WriteLine($"dest id:{dest.id}  title:{dest.title}  tags:{dest.tags} items:{dest.source_items.Count}");

            Console.WriteLine("dest to source:");
            var origin = Mapper.Map<SourceDto, Source>(dest);
            Console.WriteLine($"origin Id:{origin.Id}  Title:{origin.Title} Tags:{string.Join(",", origin.Tags)} Items:{origin.SourceItems.Count}");
        }
    }
}
