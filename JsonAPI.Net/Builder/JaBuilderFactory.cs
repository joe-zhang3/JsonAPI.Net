using System;
using System.Collections.Generic;
using System.Linq;

namespace JsonAPI.Net
{
    public enum JaBuilderType{
        Default,
        Custom,
        Uri,
        Dictionary,
        Enumerable,
        Resource,
        EnumerableRelationships
    }

    public class JaBuilderFactory
    {
        private static JaBuilderFactory factory = null;

        private IBuilder defaultBuilder;
        private IDictionary<JaBuilderType, IBuilder> builders;
        private IDictionary<Type, IBuilder> customBuilder;

        private JaBuilderFactory(JaConfiguration configuration){
            builders = new Dictionary<JaBuilderType, IBuilder>();
            ParseCustomBuilder(configuration);
            defaultBuilder = new JaDefaultBuilder();
        }

        private void ParseCustomBuilder(JaConfiguration configuration){

            customBuilder = new Dictionary<Type, IBuilder>();

            foreach(var builder in configuration?.GetBuilders()){
                customBuilder.Add(builder.CustomType, builder);
            }
        }

        public static void Initialize(JaConfiguration configuration){
            if (factory == null){
                factory = new JaBuilderFactory(configuration);
            }
        }

        public static IBuilder GetBuilder(Type type, bool buildingRelationship = false){
            return factory.GetBuilderInternal(type, buildingRelationship);
        }

        private IBuilder GetBuilderInternal(Type type, bool buildingRelationship = false){

            if(type.IsPrimitive()){

                bool? hasBuilder = customBuilder?.ContainsKey(type);

                if(hasBuilder.HasValue && hasBuilder.Value){
                    return customBuilder[type];
                }
            }

            if (type == typeof(Uri)){
                return TryToGetBuilder(JaBuilderType.Uri);
			}
			else if (type.IsGenericType)
			{
				if (typeof(IDictionary<,>).IsAssignableFrom(type.GetGenericTypeDefinition()))
				{
                    return TryToGetBuilder(JaBuilderType.Dictionary);
				}
                else if (typeof(IEnumerable<JaResource>).IsAssignableFrom(type))
				{
                     return TryToGetBuilder(buildingRelationship ? JaBuilderType.EnumerableRelationships : JaBuilderType.Enumerable);
                }else{
                     return TryToGetBuilder(JaBuilderType.Enumerable);
                }
			}
            else if (typeof(IResource).IsAssignableFrom(type))
			{
                return TryToGetBuilder(JaBuilderType.Resource);
			}
            return defaultBuilder;
        }

        private IBuilder TryToGetBuilder(JaBuilderType builderType){
            IBuilder builder = null;

            if (builders.TryGetValue(builderType, out builder)) return builder;

            switch(builderType){
                case JaBuilderType.Uri:
                    builder = new JaUrlBuilder();
                    break;
                case JaBuilderType.Dictionary:
                    builder = new JaDictionaryBuilder();
                    break;
                case JaBuilderType.Enumerable:
                    builder = new JaEnumerableBuilder();
                    break;
                case JaBuilderType.Resource:
                    builder = new JaResourceBuilder();
                    break;
                case JaBuilderType.EnumerableRelationships:
                    builder = new JaEnumerableRelationshipBuilder();
                    break;
            }

            if (builder != null && !builders.ContainsKey(builderType)) builders.Add(builderType, builder);

            return builder ?? defaultBuilder;
        }
    }
}
