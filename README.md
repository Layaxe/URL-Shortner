# URL Shortner
The scope of this project is to make a simple URL shortener that only takes a long URL and returns a shorter one, and when the short URL is used, it will redirect to the original long URL.
There will be no Aliases, Experiy URL, User related accounting.

### Project Milestones
- [ ] Implement a simple api that takes url convert to short url and redirect it (no database no storage).


### Project Design 
Modular design using Inerfaces.
Snowflake for ID's generation and Base 62 for encoding.
(still thinking) Non relational database (MongoDB) since the read will be higher than the write.
