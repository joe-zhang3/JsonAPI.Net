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
        Resource
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

        public static IBuilder GetBuilder(Type type){
            return factory.GetBuilderInternal(type);
        }

        private IBuilder GetBuilderInternal(Type type){
            if(customBuilder.TryGetValue(type, out var builder)){
                return builder;
            }

            if (type == typeof(Uri)){
                return TryToGetBuilder(JaBuilderType.Uri);
			}
			else
            {
                if (type.IsGenericType)
                {
                    if (typeof(IDictionary<,>).IsAssignableFrom(type.GetGenericTypeDefinition()))
                    {
                        return TryToGetBuilder(JaBuilderType.Dictionary);
                    }

                    return TryToGetBuilder(JaBuilderType.Enumerable);
                }
                if (typeof(IResource).IsAssignableFrom(type))
                {
                    return TryToGetBuilder(JaBuilderType.Resource);
                }
            }

            return defaultBuilder;
        }

        private IBuilder TryToGetBuilder(JaBuilderType builderType){
            if (builders.TryGetValue(builderType, out var builder)) return builder;

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
            }

            if (builder != null && !builders.ContainsKey(builderType)) builders.Add(builderType, builder);

            return builder ?? defaultBuilder;
        }
    }
}
