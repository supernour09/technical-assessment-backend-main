﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class TargetAsset
    {
        public int id { get; set; }
        public bool? isStartable { get; set; }
        public string location { get; set; }
        public string owner { get; set; }
        public string createdBy { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public List<string> tags { get; set; }
        public int cpu { get; set; }
        public long ram { get; set; }
        public DateTime createdAt { get; set; }
        public int? parentId { get; set; }
        public int parentTargetAssetCount { get; set; }
    }
}
