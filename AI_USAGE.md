# AI Usage

This project was developed with assistance from Claude (Anthropic) via claude.ai.

## How AI Was Used

Claude was used as a guided tutor throughout the development process. Rather than generating the entire project at once, the conversation was structured as a step-by-step walkthrough — designing the class structure first, then building each file one at a time with explanations along the way. All final decisions on structure and simplicity were made by the student.

---

## Prompts Used

### Planning and Setup

> "Hi and good afternoon. I need help with coding a task again. Please run me through" *(assignment document attached)*

> "I want to make mine as simple as possible. In terms of both code and no added bonus features. Can you help me"

> "I want the commit msg to sound natural" / "What commit msg" *(asked throughout for guidance on Git commit messages)*

> "Is there a way for me to send you someones repo from github so you can compare their work to the task were doing?"

> *(Panida287/Arbeidskrav2 shared for reference and inspiration)*

### Understanding Concepts

> "What is an enum"

> "/// This seems like an excessive amount of signage for a comment, compared to something like //"

> "Can you tell me step by step the functionality of the code snippet you sent. The `<summary>` for example — the contents i (the user) will see in the terminal as the program runs? Is `public DateTime DatePosted { get; }` a function to retrieve the date and time from a library?"

> "The seed/test data does what?"

> "How does this look in comparison to the one you looked at in Panidas repo"

### Code Generation

> "Yes — walk me through it file by file" *(for Marketplace.cs)*

> "Give it to me all in one go" *(for Program.cs)*

### Knowledge Verification (Student-initiated)

At the end of development, the student requested to be tested on their understanding of the code before submission. The following questions were asked by Claude and answered by the student:

> *"What is an enum and why did we use them instead of plain strings? What are the three enums we created and what does each one represent?"*

> *"What does each model class represent? What kind of data does each one hold? Why do the properties mostly only have `get` and no `set`?"*

> *"What is the purpose of `Marketplace.cs` compared to the model classes? Can you name the methods we wrote and what each one does?"*

> *"What is the role of `Program.cs` compared to `Marketplace.cs`? What is the `currentUser` variable and why is it important? What does `try/catch` do in the context of this program?"*

> *"Why do we throw exceptions instead of returning error strings?"*

The student demonstrated solid understanding of encapsulation, separation of concerns, LINQ, enums, exception handling and the overall program flow before submission.

---

**Author**: Aksel Oldeide
**Course**: Backend Programming, 1st Year — Gokstad Akademiet
