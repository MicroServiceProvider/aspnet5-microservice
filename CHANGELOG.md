Changes
=======

#### 0.5.2

- Add option to restrict actuator endpoint access to certain IP addresses

#### 0.5.1

- Add ability to opt out of displaying environment variables on the env endpoint

#### 0.5.0

- Updated to support DNX RC1 Update 1

#### 0.4.0

- Updated to support DNX beta 8

#### 0.3.1

- Modify health endpoint to return a 503 status code if any health checks fail

#### 0.3.0

- Replaced Microsoft.Framework.Logging with a custom implementation that uses a dedicated background thread for logging to improve throughput under high load

#### 0.2.1

- Added ApplicationLog static helper class to wrap around Microsoft.Framework.Logging

#### 0.2.0

- Updated to support DNX beta 7
- Removed Mac OSX specific OS detection from the environment endpoint

#### 0.1.1

- First public release to Nuget