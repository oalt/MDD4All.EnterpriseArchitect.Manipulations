# MDD4All.EnterpriseArchitect.Manipulations

[![Nuget Package Build](https://github.com/oalt/MDD4All.EnterpriseArchitect.Manipulations/actions/workflows/build.yml/badge.svg)](https://github.com/oalt/MDD4All.EnterpriseArchitect.Manipulations/actions/workflows/build.yml)
[![NuGet version](https://badge.fury.io/nu/MDD4All.EnterpriseArchitect.Manipulations.svg?icon=si%3Anuget)](https://badge.fury.io/nu/MDD4All.EnterpriseArchitect.Manipulations)

This project contains a collection of Enterprise Architect API helpers for the modeling platform [Spax Systems Enterprise Architect](https://www.sparxsystems.com) (EA). 
It is realized as a collection of extension methods for the existing Sparx Systems Enterprise Architect API (Interop.EA.dll).

By using the extension methods, it is much easier to manipulate model elements using the EA-API.

## Example
The following code is normally necessary to add a new model element in an existing package (create the element and call update and refresh to update the data base):

```C#
using EAAPI = EA;
...

EAAPI.Element newElement = (EAAPI.Element)parentPackage.Elements.AddNew(name, type);

newElement.Update();
parentPackage.Elements.Refresh();
parentPackage.Element.Refresh();
```

With the following extension method

```C#
public static EAAPI.Element AddElement(this EAAPI.Package parentPackage, string name, string type)
{
    EAAPI.Element newElement = (EAAPI.Element)parentPackage.Elements.AddNew(name, type);

    newElement.Update();
    parentPackage.Elements.Refresh();
    parentPackage.Element.Refresh();

    return newElement;
}
```

the API user can now create a model element with just one line of code:

```C#
EA.Package package = <code to get a package object>;

// now we use the extension method to create a new class element with the class name 'Person'
package.AddElement("Person", "Class");
```
