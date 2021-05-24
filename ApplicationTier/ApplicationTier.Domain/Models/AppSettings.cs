using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationTier.Domain.Models
{
    public class AppSettings
    {
        public static string ConnectionString { get; private set; }
        public static string[] CORS { get; private set; }
    }
}
