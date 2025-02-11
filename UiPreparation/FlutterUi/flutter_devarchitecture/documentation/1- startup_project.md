# Project Setup and Startup Guide

This document provides detailed information about project setup, how to start it in different environments, and how to build it.

## Table of Contents

- [Project Setup and Startup Guide](#project-setup-and-startup-guide)
  - [Table of Contents](#table-of-contents)
  - [Flutter Installation](#flutter-installation)
  - [Project Setup](#project-setup)
  - [Starting the Project](#starting-the-project)
    - [Development Environment](#development-environment)
    - [Staging Environment](#staging-environment)
    - [Production Environment](#production-environment)
    - [Production Environment with Firebase](#production-environment-with-firebase)
    - [Production Environment without Firebase](#production-environment-without-firebase)
  - [Build Commands](#build-commands)
    - [Web](#web)
    - [Android APK](#android-apk)
    - [Windows](#windows)
    - [macOS (ARM64)](#macos-arm64)
    - [macOS (x86\_64)](#macos-x86_64)
  - [Environment Configurations](#environment-configurations)
  - [Firebase Usage](#firebase-usage)
  - [Troubleshooting](#troubleshooting)

## Flutter Installation

1. Download and install the latest stable version of Flutter from the [Flutter official website](https://flutter.dev/docs/get-started/install).

2. Recommended Flutter version: 3.22.3

   To install a specific Flutter version, run the following command in the terminal or command prompt:

   - flutter version 3.22.3

3. To verify that the installation was successful, run the following command:

   ```bash
   flutter doctor
   ```

   This command will show any missing dependencies or configurations in your Flutter installation.

4. Install all dependencies and complete the necessary configurations.

## Project Setup

1. Clone the project repository:

   ```bash
   git clone [PROJECT_REPO_URL]
   ```

2. Navigate to the project directory:

   ```bash
   cd [PROJECT_NAME]
   ```

3. Install the required dependencies:

   ```bash
   flutter pub get
   ```

## Starting the Project

The project can be started with different configurations for different environments. Below are the commands for each environment:

### Development Environment

```bash
flutter run --dart-define ENV=dev
```

### Staging Environment

```bash
flutter run --dart-define ENV=staging
```

### Production Environment

```bash
flutter run --dart-define ENV=prod
```

### Production Environment with Firebase

```bash
flutter run --dart-define ENV=prod --dart-define=FIREBASE=true
```

### Production Environment without Firebase

```bash
flutter run --dart-define ENV=prod --dart-define=FIREBASE=false
```

## Build Commands

Build commands for different platforms:

### Web

```bash
flutter build web --dart-define ENV=staging --web-renderer html --release
```

### Android APK

```bash
flutter build apk --dart-define ENV=prod --release
```

### Windows

```bash
flutter build windows --dart-define ENV=prod --release
```

### macOS (ARM64)

```bash
flutter build macos --dart-define ENV=prod --dart-define=FIREBASE=true --verbose --target-arch=arm64 --release
```

### macOS (x86_64)

```bash
flutter build macos --dart-define ENV=prod --dart-define=FIREBASE=true --verbose --target-arch=x86_64 --release
```

## Environment Configurations

The project uses different configurations for different environments. These configurations are managed by the `AppConfig` class.

- `dev`: Development environment configuration
- `staging`: Test environment configuration
- `prod`: Production environment configuration

There is a separate configuration file for each environment:

- `dev_config.dart`
- `staging_config.dart`
- `prod_config.dart`

The environment is determined by the `--dart-define ENV=<environment>` parameter.

## Firebase Usage

Firebase usage is controlled by the `--dart-define=FIREBASE=true` or `--dart-define=FIREBASE=false` parameter.

- `--dart-define=FIREBASE=true`: Firebase is used
- `--dart-define=FIREBASE=false`: Firebase is not used

When this parameter is not specified, Firebase is used by default.

## Troubleshooting

1. If you experience issues while installing dependencies, try the following command:

   ```bash
   flutter pub cache repair
   flutter pub get
   ```

2. If you encounter errors during the build process, first clean the project:

   ```bash
   flutter pub cache clean
   flutter clean
   flutter pub get
   ```

   Then try the build process again.

3. If you experience platform-specific issues, make sure that the development tools for the relevant platform are correctly installed. For example, Android Studio is required for Android, and Xcode is required for iOS.

4. If you encounter any problems, please contact the project manager or technical team.

Note: The commands and configurations specified in this document are based on the current structure of the project. This document should be updated if there are changes in the project structure or requirements.
