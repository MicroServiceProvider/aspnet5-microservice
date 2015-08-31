# ASP.NET 5 Microservice

A library containing a small set of classes that add actuator style endpoints to an ASP.NET 5 application. Works on both Windows and Linux and will run on either .NET Framework, Mono or the .NET CoreCLR.

This package has only been tested against DNX beta6 but I will continue to update the package as future betas are pushed to the Nuget.org repository by the ASP.NET team.

## Installation

Run the following from the package manager console to install the package

```
PM> Install-Package aspnet5-microservice -Pre
```

Pre-release option is currently required as the ASP.NET 5 packages are still pre-release packages.

One of the packages this library depends on to run under .NET Core is not yet available in the Nuget.org repo so you need to add the ASP.NET VNext MyGet feed if you plan to target .NET Core.

#### Feed URL
```
https://www.myget.org/F/aspnetvnext/
```

## Documentation

Documentation is currently lacking but will be available on the Wiki as it is added. The sample project does contain fully commented code.

## Sample

A working sample is included in this repo to use it follow these steps:

1. Install DNVM as described at [This link](https://github.com/aspnet/Home)

2. Install the DNX beta6 runtime

#### For full CLR / Mono
```
dnvm install 1.0.0-beta6
```

#### For CoreCLR
```
dnvm install 1.0.0-beta6 -r coreclr
```

3. Change into sample project directory and run the following to restore packages from Nuget

```
dnu restore
```

4. Start the application by running one of the below depending on platform from the sample project directory

#### Windows
```
dnx . web
```

#### Linux / Mac OSX
```
dnx . kestrel
```

