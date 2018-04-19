using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Laboratory.WebApi.Models.Parameters
{
    [Serializable]
    public class DataParameter
    {
        public DataParameter()
        {
            this.DataItems = new List<DataItemParameter>();
        }

        public int Id { get; set; }

        public List<DataItemParameter> DataItems { get; set; }
    }

    [Serializable]
    public class DataItemParameter
    {
        public DataItemParameter() { }
        public DataItemParameter(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        [Range(10, 20)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<DataItemProperty> Properties { get; set; }
    }

    [Serializable]
    public class DataItemProperty
    {
        public string Name { get; set; }
    }
}