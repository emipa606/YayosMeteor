# GitHub Copilot Instructions for Yayo's Meteor (Continued) Mod

## Mod Overview and Purpose
Yayo's Meteor (Continued) is an update to YAYO's original mod, adding new features and enhancing the gameplay experience. The mod introduces giant meteor showers that occur within 7-10 seconds after the alarm goes off, requiring players to seek shelter. Occasionally, these meteors may leave behind a generator. The mod can be activated after at least 45 days and may occur once or twice a year thereafter.

## Key Features and Systems
- **Giant Meteor Showers:** Players must seek shelter from giant meteor showers that occur after an alarm.
- **Generator Drop:** Occasionally, meteors may leave behind a generator.
- **Delay Time Adjustment:** Players can adjust the delay time before the meteor drops through the mod's options.

## Coding Patterns and Conventions
- **C# Coding Style:** Follow standard C# conventions, including naming conventions (PascalCase for classes, methods, and properties; camelCase for local variables).
- **XML Conventions:** Use consistent and clear naming in XML files, ensuring that all tags and attributes are correctly formatted.
- **Error Handling:** Implement proper error handling to manage exceptions and unexpected behavior within the mod.

## XML Integration
- **Def Files:** Use XML definition files to define new game conditions and incidents. These files are located in the `Defs` directory.
- **Translation Files:** Include translation files to support multiple languages. These files are located in the `TranslationTemplate` directory.

## Harmony Patching
- **Harmony Patches:** Utilize Harmony patching to modify existing RimWorld functionality. Ensure that patches are well-documented and maintainable.
- **Patch Structure:** Organize patches into separate classes, each responsible for a specific aspect of the mod's functionality.

## Suggestions for Copilot
- **Code Generation:** Request Copilot to generate new classes, methods, and XML definitions based on existing patterns.
- **Error Handling:** Ask Copilot to provide suggestions for error handling and exception management.
- **Harmony Patch Examples:** Request Copilot to provide examples of Harmony patches for modifying RimWorld functionality.
- **Mod Configuration:** Ask Copilot to generate code for mod configuration options, such as the delay time before the meteor drops.

## Example Requests for Copilot
- **Generate a new Harmony patch to modify the meteor drop delay.**
- **Provide an example of how to implement a new game condition using XML.**
- **Suggest improvements to the existing error handling in the mod.**
- **Generate a new XML definition for a new meteor-related incident.**

By following these guidelines, developers can effectively utilize GitHub Copilot to enhance and expand the functionality of Yayo's Meteor (Continued) mod.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

