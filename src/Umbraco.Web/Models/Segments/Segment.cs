using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Umbraco.Web.Models.Segments
{
    /// <summary>
    /// Defines an assigned segment in a request
    /// </summary>
    /// <remarks>
    /// The serialization names are only one letter - this is intentional to keep the cookie size small
    /// </remarks>
    [DataContract(Name = "s", Namespace = "")]
    public class Segment
    {
        /// <summary>
        /// Constructor, required for deserialization
        /// </summary>
        public Segment()
        {
        }

        public Segment(string key, object value)
        {
            Key = key;
            Value = value;
        }

        public Segment(string key, object value, bool persist)
        {
            Key = key;
            Value = value;
            Persist = persist;
        }

        internal Segment(string key, object value, bool persist, int? slidingDays)
        {
            Key = key;
            Value = value;
            Persist = persist;
            SlidingDays = slidingDays;
        }

        internal Segment(string key, object value, bool persist, DateTime? absoluteExpiry)
        {
            Key = key;
            Value = value;
            Persist = persist;
            AbsoluteExpiry = absoluteExpiry;
        }

        /// <summary>
        /// The name of the segment
        /// </summary>
        [DataMember(Name = "k", IsRequired = true)]
        public string Key { get; set; }

        /// <summary>
        /// The value of the segment
        /// </summary>
        [DataMember(Name = "v")]
        public object Value { get; set; }

        /// <summary>
        /// Whether or not this segment is to be persisted (default is false)
        /// </summary>
        [DataMember(Name = "p")]
        public bool Persist { get; set; }

        //TODO: We should make use of these expiry settings!

        /// <summary>
        /// Defines the sliding expiration date in days from now for this particular segment
        /// </summary>
        [JsonIgnore]
        [IgnoreDataMember]
        internal int? SlidingDays { get; private set; }

        /// <summary>
        /// Defines the absolute expiration date for this particular segment
        /// </summary>
        [JsonIgnore]
        [IgnoreDataMember]
        internal DateTime? AbsoluteExpiry { get; private set; }

        protected bool Equals(Segment other)
        {
            return string.Equals(Key, other.Key);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Segment) obj);
        }

        public override int GetHashCode()
        {
            return Key.GetHashCode();
        }
    }
}