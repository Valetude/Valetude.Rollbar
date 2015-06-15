# Valetude.Rollbar
A .NET Rollbar Client that is not ASP.NET specific.

## Install

Nuget Package Manager:

    install-package Valetude.Rollbar

## Use

 1. Create a `RollbarPayload` object (The library is constructed to guarantee that if you can construct it, the payload will be valid to POST to rollbar).
 2. Send the result of `RollbarPayload.ToJson()` to Rollbar using whatever library you prefer. (I like `RestSharp`).
