# ASP.NET 5 Microservice

A library containing a small set of classes that add actuator style endpoints to an ASP.NET 5 application. Works on both Windows and Linux and will run on either .NET Framework, Mono or the .NET CoreCLR.

This package has only been tested as far as DNX RC1_update1 but I will continue to update the package as future betas are pushed to the Nuget.org repository by the ASP.NET team.

Code is licensed under the ISC license to use as you wish.

## Installation

Run the following from the package manager console to install the package

```
PM> Install-Package aspnet5.microservice
```

## Documentation

Documentation is currently lacking but will be available on the Wiki as it is added. The sample project does contain fully commented code.

## Sample

A working sample is included in this repo to use it follow these steps:

Install DNVM as described at [This link](https://github.com/aspnet/Home)

Install the DNX runtime

#### For full CLR / Mono
```
dnvm install 1.0.0-rc1-update1
```

#### For CoreCLR
```
dnvm install 1.0.0-rc1-update1 -r coreclr
```

Change into sample project directory and run the following to restore packages from Nuget

```
dnu restore
```

Start the application by running the below from the sample project directory

```
dnx . kestrel
```

The following endpoints will be available:

- [http://localhost:5000/info](http://localhost:5000/info)
- [http://localhost:5000/health](http://localhost:5000/health)
- [http://localhost:5000/env](http://localhost:5000/env)
