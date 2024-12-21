# Use a limit extension

The `ModelParser` class is used to determine whether one or more DTDL models are valid, to identify specific modeling errors, and to enable inspection of model contents.
This tutorial walks through an aspect of the first use: determining model validity for applications wherein models can exceed the standard limits of the DTDL language.

## Create a ModelParser

To parse a DTDL model, you need to instantiate a `ModelParser`.
No arguments are required.

```C# Snippet:DtdlParserTutorial17_CreateModelParser
var modelParser = new ModelParser();
```

## Obtain the JSON text of a DTDL model that exceeds the standard limits

The DTDL language is syntactically JSON.
The `ModelParser` expects a single string or an enumeration of strings.
The single string or each value in the enumeration is JSON text of a DTDL model.
The following model defines a 10-dimensional Array, which is beyond the maximum schema depth permitted in DTDL v4.

```C# Snippet:DtdlParserTutorial17_ObtainDtdlText
string jsonText =
@"{
  ""@context"": ""dtmi:dtdl:context;4"",
  ""@id"": ""dtmi:example:anInterface;1"",
  ""@type"": ""Interface"",
  ""schemas"": [
    {
      ""@id"": ""dtmi:example:deepArray;1"",
      ""@type"": ""Array"",
      ""elementSchema"": {
        ""@type"": ""Array"",
        ""elementSchema"": {
          ""@type"": ""Array"",
          ""elementSchema"": {
            ""@type"": ""Array"",
            ""elementSchema"": {
              ""@type"": ""Array"",
              ""elementSchema"": {
                ""@type"": ""Array"",
                ""elementSchema"": {
                  ""@type"": ""Array"",
                  ""elementSchema"": {
                    ""@type"": ""Array"",
                    ""elementSchema"": {
                      ""@type"": ""Array"",
                      ""elementSchema"": {
                        ""@type"": ""Array"",
                        ""elementSchema"": ""double""
                      }
                    }
                  }
                }
              }
            }
          }
        }
      }
    }
  ]
}";
```

This is not a very useful model, but it is sufficient to illustrate the point of this tutorial.

## Submit the JSON text to the ModelParser

The main synchronous method on the `ModelParser` is `Parse()`.
One argument is required, which can be either a string or an enumeration of strings containing the JSON text to parse as DTDL.
If the submitted model is invalid, a `ParsingException` will be thrown.

```C# Snippet:DtdlParserTutorial17_CallParse
try
{
    var objectModel = modelParser.Parse(jsonText);
    Console.WriteLine($"DTDL model is valid!");
}
catch (ResolutionException ex)
{
    Console.WriteLine($"DTDL model is referentially incomplete: {ex}");
}
catch (ParsingException ex)
{
    Console.WriteLine("DTDL model is invalid:");
    foreach (ParsingError err in ex.Errors)
    {
        Console.WriteLine(err);
    }
}
```

## Observe parsing errors

The `ParsingException` has a property named `Errors` that is a collection of `ParsingError` objects, each of which provides details about one error in the submitted model.
This class also overrides the `ToString()` method to provide a human-readable description of the error.

For the JSON text above, the code snippet above will display the following:

```Console
DTDL model is invalid:
dtmi:example:deepArray;1 has 'elementSchema' value which is at the root of a hierarchy that exceeds 8 levels -- element is at level 9. Change the value of one or more properties of elements in the hierarchy to reduce the nesting depth.
dtmi:example:deepArray;1 is at the root of a hierarchy that exceeds 8 levels -- element is at level 9. Change the value of one or more properties of elements in the hierarchy to reduce the nesting depth.
```

The schema definition "dtmi:example:deepArray;1" contains 10 levels of nested schema definitions.
This exceeds the DTDL v4 standard limit of 8 nested levels of schema definitions, so it is flagged as an error by the parser.

A 10-level nested definition contains a 9-level nested definition, which in turn contains an 8-level definition, and so on.
The 9-level nested definition also exceeds the DTDL v4 standard limit, so this definition is also flagged as an error.
The definition is the value of the "elementSchema" property of "dtmi:example:deepArray;1".

## Specify the Onvif limit extension and resubmit

Beginning with DTDL v4, a model is able to specify a *limit extension* to indicate the set of limits the model is written against.
Limit extensions define increased values for one or more numerical limits on DTDL property values.
In particular, the Onvif limit extension increases the maximum nested schema depth to 24 levels, which is far beyond the schema depth permitted by the standard DTDL v4 limits.

To use a limit extension, a model includes the context specfier for the extension, which for the Onvif extension is "dtmi:dtdl:limits:onvif;1".
However, the model must not also indicate the standard DTDL limits, which are defined by the DTDL language definitions that are referenced by the DTDL context, such as "dtmi:dtdl:context;4" for DTDL v4.
Instead, the model should reference the subset of the DTDL language definitions that do not define any limits.
This is done by appending an IRI fragment to the DTDL context specifier to indicate the "limitless" subset of the DTDL language.
This is then followed by the context specifier for the limit extension, as shown below.

```C# Snippet:DtdlParserTutorial17_AddOnvifContext
jsonText =
@"{
  ""@context"": [
    ""dtmi:dtdl:context;4#limitless"",
    ""dtmi:dtdl:limits:onvif;1""
  ],
  ""@id"": ""dtmi:example:anInterface;1"",
  ""@type"": ""Interface"",
  ""schemas"": [
    {
      ""@id"": ""dtmi:example:deepArray;1"",
      ""@type"": ""Array"",
      ""elementSchema"": {
        ""@type"": ""Array"",
        ""elementSchema"": {
          ""@type"": ""Array"",
          ""elementSchema"": {
            ""@type"": ""Array"",
            ""elementSchema"": {
              ""@type"": ""Array"",
              ""elementSchema"": {
                ""@type"": ""Array"",
                ""elementSchema"": {
                  ""@type"": ""Array"",
                  ""elementSchema"": {
                    ""@type"": ""Array"",
                    ""elementSchema"": {
                      ""@type"": ""Array"",
                      ""elementSchema"": {
                        ""@type"": ""Array"",
                        ""elementSchema"": ""double""
                      }
                    }
                  }
                }
              }
            }
          }
        }
      }
    }
  ]
}";
```

[repeat]: # (Snippet:DtdlParserTutorial17_CallParse)

When this revised JSON text is submitted via the code snippet above, it displays:

```Console
DTDL model is invalid:
@context specifier 'dtmi:dtdl:limits:onvif;1' is not an acceptable limit extension. Replace the @context specifier with an acceptable limit context value: "dtmi:dtdl:context;4#limits".
```

## Specify the standard DTDL limits via a limit context and resubmit

At this point, the parser is not complaining that the model exceeds a limit.
The parser is complaining that the model specifies a limit extension that is not acceptable.
The error message indicates the limit extension values that would be acceptable, of which there is exactly one: "dtmi:dtdl:context;4#limits".
This is the context specifier for the subset of the DTDL language that defines the standard limits, as indicated by the IRI fragment "limits" at the end of the context DTMI.

Let's see what happens if we replace the limit context "dtmi:dtdl:limits:onvif;1" with "dtmi:dtdl:context;4#limits", per the recommendation in the error message:

```C# Snippet:DtdlParserTutorial17_StandardLimitContext
jsonText =
@"{
  ""@context"": [
    ""dtmi:dtdl:context;4#limitless"",
    ""dtmi:dtdl:context;4#limits""
  ],
  ""@id"": ""dtmi:example:anInterface;1"",
  ""@type"": ""Interface"",
  ""schemas"": [
    {
      ""@id"": ""dtmi:example:deepArray;1"",
      ""@type"": ""Array"",
      ""elementSchema"": {
        ""@type"": ""Array"",
        ""elementSchema"": {
          ""@type"": ""Array"",
          ""elementSchema"": {
            ""@type"": ""Array"",
            ""elementSchema"": {
              ""@type"": ""Array"",
              ""elementSchema"": {
                ""@type"": ""Array"",
                ""elementSchema"": {
                  ""@type"": ""Array"",
                  ""elementSchema"": {
                    ""@type"": ""Array"",
                    ""elementSchema"": {
                      ""@type"": ""Array"",
                      ""elementSchema"": {
                        ""@type"": ""Array"",
                        ""elementSchema"": ""double""
                      }
                    }
                  }
                }
              }
            }
          }
        }
      }
    }
  ]
}";
```

[repeat]: # (Snippet:DtdlParserTutorial17_CallParse)

When we submit this to the code snippet above, the output is:

```Console
DTDL model is invalid:
dtmi:example:deepArray;1 has 'elementSchema' value which is at the root of a hierarchy that exceeds 8 levels -- element is at level 9. Change the value of one or more properties of elements in the hierarchy to reduce the nesting depth.
dtmi:example:deepArray;1 is at the root of a hierarchy that exceeds 8 levels -- element is at level 9. Change the value of one or more properties of elements in the hierarchy to reduce the nesting depth.
```

This is the same error message we saw before, when the context was "dtmi:dtdl:context;4".
This is because the contexts "dtmi:dtdl:context;4#limitless" and "dtmi:dtdl:context;4#limits", taken together, say to include the limitles subset of the DTDL v4 language and the limits subset of the DTDL v4 language.
When combined, these two subsets comprise the entire DTDL language, so this is equivalent to the context "dtmi:dtdl:context;4".

## Configure the parser to accept the Onvif limit extension and resubmit

The problem we saw above when using the Onvif limit context was not really in the model.
The error message said the model was invalid, but it was only invalid by virtue of the parser's configuration.
In common scenarios, model authors will not have access to the parser directly but will instead be using a service or tool that employs the parser with a pre-set configuration.
In such scenarios, the error message accurately conveys to the model author that the model is not valid for the given service or tool, and it lists the limit contexts that are acceptable, which by default includes only the standard DTDL limits.

If the maintainer of a service or tool has validated that higher limits will not pose a problem, the maintainer can configure the parser to accept limit extensions whose defined limits are tolerable to the service or tool.
This is done via tha `ParsingOptions` class, which is used to set options for `ModelParser` behavior.

The following snippet instantiates an instance of the `ParsingOptions` class, adds a DTMI for the Onvif limit extension to the `ExtensionLimitContexts` property, and instantiates a new `ModelParser` with the configured options:

```C# Snippet:DtdlParserTutorial17_NewParserWithOptions
ParsingOptions parsingOptions = new ParsingOptions();
parsingOptions.ExtensionLimitContexts.Add(new Dtmi("dtmi:dtdl:limits:onvif"));
modelParser = new ModelParser(parsingOptions);
```

The DTMI that is added to `ExtensionLimitContexts` is "dtmi:dtdl:limits:onvif", which is a versionless form of the Onvif limit extension context "dtmi:dtdl:limits:onvif;1".
When an `ExtensionLimitContexts` value has no version suffix, this indicates that all major/minor versions of the indicated limit extension are acceptable.
In the above case, the parser would accept not only "dtmi:dtdl:limits:onvif;1", but also "dtmi:dtdl:limits:onvif;2", "dtmi:dtdl:limits:onvif;3", or "dtmi:dtdl:limits:onvif;3.14" if the given version has been defined.

When an `ExtensionLimitContexts` value has a version suffix, the parser will accept the indicated major version and all equal or greater minor versions.
For example, a value of "dtmi:dtdl:limits:onvif;2.2" would accept "dtmi:dtdl:limits:onvif;2.2", "dtmi:dtdl:limits:onvif;2.3", or "dtmi:dtdl:limits:onvif;3", if the given version has been defined, but not "dtmi:dtdl:limits:onvif;1", "dtmi:dtdl:limits:onvif;2", or "dtmi:dtdl:limits:onvif;2.1".

Now, we can resumbit the DTDL model with the Onvif context to the parser.

[repeat]: # (Snippet:DtdlParserTutorial17_AddOnvifContext)

[repeat]: # (Snippet:DtdlParserTutorial17_CallParse)

The Onvif extension is acceptable to the parser because of the `ExtensionLimitContexts` addition, and the model does not exceed the Onvif limits, so the code snippet above will display:

```Console
DTDL model is valid!
```
