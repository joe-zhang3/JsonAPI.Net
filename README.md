# JsonAPI.Net (WIP)

## Summary
This is a .net library used to build up a json response which meets the JSON API Specification v1. This is still under going and I will keep it updated as my project is going to use it.

The general idea is use a resource object to fill up a json schema. (Newsoft.Json.Schema might be a choice but is not free for commerical use, so it is one reason why I created this project). 

When you have a json template, what you need to do is try to fill those values with the resource object, which gives you a huge flexiblity to do whatever you want. 

## usage

1. Simply call the config.ConfigureJsonApi to inject the media type formatter. 
2. A scanner will scan all .json files under the "Templates" folder and cache them.
3. A resource object and extends the base class JaResource.
4. Add the JaResource attribute to your resource POCO with the template name specified. 

`{{key}}` - tells the library this is a placeholder. 
`{%templateName%}` - tells the library this is a reference to other template.

## Roadmap 


## License
This is under MIT license.




