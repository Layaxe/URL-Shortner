# URL Shortner
The scope of this project is to make a simple URL shortener that only takes a long URL and returns a shorter one, and when the short URL is used, it will redirect to the original long URL.
There will be no Aliases, Experiy URL, User related accounting.

### Project Milestones
- [X] Implement a simple api that takes url convert to short url and redirect it (no database no storage).
- [ ] Implement a code generation service and use it with the current code flow.
- [ ] Add database storage and use it.
- [ ] Containerize the project.
- [ ] Deploy ? Profit ??


### Project Design 
Modular design using interfaces.
The code generation will be modular so we can replace it easily; the current focus will be Snowflake for ID generation and Base62 for encoding, but if it turns out to be complicated,
another generation method will be used.
The database depends on the use case and needs, but Non-relational databases work well with this kind of heavy read service; relational databases also work fine and could be a better choice. 
If auto-increment were the method to generate code.
