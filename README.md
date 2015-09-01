# ASP.NET 5 Microservice

A library containing a small set of classes that add actuator style endpoints to an ASP.NET 5 application. Works on both Windows and Linux and will run on either .NET Framework, Mono or the .NET CoreCLR.

This package has only been tested against DNX beta6 but I will continue to update the package as future betas are pushed to the Nuget.org repository by the ASP.NET team.

Code is licensed under the ISC license to use as you wish.

## Installation

Run the following from the package manager console to install the package

```
PM> Install-Package aspnet5.microservice
```

One of the packages this library depends on to run under .NET Core is not yet available in the Nuget.org repo so you need to add the ASP.NET VNext MyGet feed if you plan to target .NET Core.

#### Feed URL
```
https://www.myget.org/F/aspnetvnext/
```

## Documentation

Documentation is currently lacking but will be available on the Wiki as it is added. The sample project does contain fully commented code.

## Sample

A working sample is included in this repo to use it follow these steps:

Install DNVM as described at [This link](https://github.com/aspnet/Home)

Install the DNX beta6 runtime

#### For full CLR / Mono
```
dnvm install 1.0.0-beta6
```

#### For CoreCLR
```
dnvm install 1.0.0-beta6 -r coreclr
```

Change into sample project directory and run the following to restore packages from Nuget

```
dnu restore
```

Start the application by running one of the below depending on platform from the sample project directory

#### Windows
```
dnx . web
```

#### Linux / Mac OSX
```
dnx . kestrel
```

The following endpoints will be available:

- [http://localhost:5000/info](http://localhost:5000/info)
- [http://localhost:5000/health](http://localhost:5000/health)
- [http://localhost:5000/env](http://localhost:5000/env)
