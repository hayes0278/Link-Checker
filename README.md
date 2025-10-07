# Link Checker
A lightweight CLI utility written in C# that checks web pages for broken links.

## Features

- Detects broken links (403, 500 errors).
- Handles both absolute and relative URL's.
- Skips non-HTTP links (mailto, tel etc).
- Fast link checking using HTTP HEAD.
- Requires no external libraries or dependencies.

## Screenshots 
![Link Checker Screenshot A]()<br>
![Link Checker Screenshot B]()<br>
![Link Checker Screenshot C]()<br>

## Examples
```
cmd> [URL]
```

## To Run
After downloading the source code, then double click on the ???.

## How It Works

- Fetches the HTML content of the given URL.
- Extracts all links located on the page.
- Converts relative URL's to absolute URL's.
- Checks each links HTTP status code
- Reports broken links (non-200 status codes)

## Limitations

- Does not handle JavaScript-rendered links.
- May miss some dynamically generated URL's.
- Rate limits are not implemented, so be careful with large websites.