# JsonAPI.Net (WIP)

## Summary
This is a .net library used to build up a json response which meets the JSON API Specification v1. This is still under going and I will keep it updated as my project is going to use it.

The general idea is use a resource object to fill up a json schema. (Newsoft.Json.Schema might be a choice but is not free for commerical use, so it is one reason why I created this project). 

When you have a json template, what you need to do is try to fill those values with the resource object, which gives you a huge flexiblity to do whatever you want. 

## Usage

1. Simply call the config.ConfigureJsonApi method to do the initialization and injection.
2. A scanner will scan all .json files under the "Templates" folder and cache them. You can specify a different name in step 1
3. A resource object and extends the base class JaResource.
4. Add the JaResource attribute to your resource POCO with the template name specified. 

## Sample



`{{key}}` - indicate this is a placeholder and the library will try to find the value in the resourc object and fill it. 
`{%templateName%}` - tells the library this is a reference to other template.

## Roadmap 


## License
This is under MIT license.




