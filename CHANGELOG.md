Changes
=======

#### 0.3.0

- Replaced Microsoft.Framework.Logging with a custom implementation that uses a dedicated background thread for logging to improve throughput under high load

#### 0.2.1

- Added ApplicationLog static helper class to wrap around Microsoft.Framework.Logging

#### 0.2.0

- Updated to support DNX beta 7
- Removed Mac OSX specific OS detection from the environment endpoint

#### 0.1.1

- First public release to Nuget