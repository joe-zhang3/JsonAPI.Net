using System;
using System.Linq;
using System.Collections.Generic;
using Humanizer;
using Newtonsoft.Json.Linq;

namespace JsonAPI.Net
{
    public class JaResourceBase 
    {
        public virtual string Id { get; set; }

        private ICollection<ILink> links;
        private ICollection<ILink> relatedLinks;

		private IDictionary<string, object> meta = new Dictionary<string, object>();
		
		public IDictionary<string, object> Meta { get { return meta; } }

        public virtual ICollection<ILink> Links { 
            get {
                return links ?? BuildDefaultLinks();
            } 
        }

        /// <summary>
        /// Called for building links when this resource is referenced by others 
        /// </summary>
        /// <value>The related links.</value>
        public virtual ICollection<ILink> RelatedLinks
		{
			get{
				return relatedLinks ?? BuildDefaultLinks() ;
			}
		} 

        protected string tempName;
        protected string type ;
        protected string url;
		/// <summary>
		/// Template name must be pascal, like Accounts
		/// </summary>
		/// <returns>The template.</returns>
		/// <param name="templateName">Template name.</param>
		public JaResourceBase OfTemplate(string templateName){
			this.tempName = templateName;
			return this;
		}

		public JaResourceBase OfType(string type)
		{
			this.type = type;
			return this;
		}
		public JaResourceBase OfURL(string url)
		{
			this.url = url;
			return this;
		}

        public void OfLink(ILink link){
            if (links == null) links = new List<ILink>();

            links.Add(link);
        }

		/// <summary>
		/// Override with your links collection
		/// </summary>
		/// <returns>The links.</returns>
        public virtual void OfRelatedLinks(ICollection<ILink> links)
		{
            this.relatedLinks = links;
		}

        /// <summary>
        /// Override with your customized self link
        /// </summary>
        /// <returns>The self link.</returns>
        public virtual IList<ILink> BuildDefaultLinks(){
            return new List<ILink>() { new JaSimpleLink("self", new Uri($"/{ url ?? Type }/{Id ?? string.Empty}")) };
        }

		/// <summary>
		/// By default, the type is the Pluralize of the class name
		/// </summary>
		/// <value>The type.</value>
		public string Type
		{
			get
			{
				if (type != null) return type;

				return GetType().Name.ToLower().Pluralize();
			}
		}

		public virtual JToken Serialize(JaBuilderContext context){
            throw new NotImplementedException();
		}

        public JContainer GetContainer(){
            return new JArray();
        }

        public static JToken Deserialize(JToken token){

            JToken tmp = token["data"] ?? token;

            if(tmp is JArray){
                JArray array = new JArray();

                foreach(var o in (JArray)tmp){
                    array.Add(ParseObject(o));
                }

                return array;

            }else{
                return ParseObject(tmp);   
            }
        }
        /// <summary>
        /// Only parse id, attributes
        /// </summary>
        /// <returns>The object.</returns>
        /// <param name="token">Token.</param>
        private static JObject ParseObject(JToken token){

            JObject obj = new JObject();

            if (token["id"] != null) obj["id"] = token["id"];

            foreach (var attr in token["attributes"] ?? new JArray()){
				var property = attr as JProperty;

                if (property == null) continue;

                obj.Add(property.Name.ToPascal(), property.Value);
			}

			foreach (var r in token["relationships"] ?? new JArray())
			{
				var relationship = r as JProperty;

                if (relationship == null) continue;

                obj.Add(relationship.Name.ToPascal(), Deserialize(relationship.Value));
			}

            return obj;
        }
    }
}
