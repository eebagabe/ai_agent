# ai_agent

CLI chatbot that sends a prompt to Gemini (`gemini-2.5-flash`), plus sandboxed functions to list, read and write files inside a working directory. The `calculator` folder is a sample app used as that working directory.

## Requirements

- .NET SDK 8.0
- A Gemini API key

## Setup

Copy `.env.example` to `.env` and fill in your key:

```dotenv
GEMINI_API_KEY='your-key'
```

## Run

```bash
dotnet run --project src/AiAgent -- "Why is the sky blue?"
dotnet run --project src/AiAgent -- "Why is the sky blue?" --verbose
```

Calculator:

```bash
dotnet run --project calculator -- "3 + 5"
```

## Tests

```bash
dotnet test
```
