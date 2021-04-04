from pandocfilters import toJSONFilter, Para, Str
import sys
import requests
import json
import logging

"""
Pandoc filter that executes code-blocks, which 
are marked with the "remote-compile" class on a 
remote server. The code's output on stdout is 
then returned.
"""

REMOTE_COMPILE_URL = "https://localhost:44301"

logging.basicConfig(stream=sys.stderr, level=logging.DEBUG)


def execute_code(code, stdin, args, language, language_version, language_file_extension):
    url = REMOTE_COMPILE_URL + "/Api/Compile/" + language + "/" + language_version
    headers = {"content-type": "application/json"}
    main_file = "main." + language_file_extension

    payload = json.dumps({
        "args": args,
        "stdin": stdin,
        "mainFile": main_file,
        "files": [
            {
                "name": main_file,
                "content": code
            }
        ]
    })

    logging.debug("Sending remote compiling request to " + url)
    response = requests.post(url, data=payload, headers=headers, verify=False)
    logging.debug("Received remote compiling response with status " + str(response.status_code) + " " + response.reason)

    if response.status_code != 200:
        raise RuntimeError(response.text)

    return json.loads(response.text)


def remote_compile(key, value, format_, meta):
    if key != 'CodeBlock':
        return

    [[ident, classes, properties], code] = value
    properties = dict(properties)
    property_names = properties.keys()

    if 'remote-compile' not in classes:
        return

    if 'language' not in property_names:
        raise SyntaxError('Remote-compile code-block is missing the "language" property. ' +
                          'Cannot determine which language the code is written in.')

    if 'language-version' not in property_names:
        raise SyntaxError('Remote-compile code-block is missing the "language-version" property. ' +
                          'Cannot determine which version of language "' + properties['language'] + '" ' +
                          'the code is written in.')

    language = properties['language']
    language_version = properties['language-version']
    stdin = ""
    args = []

    file_extension_switcher = {
        "cs":      ".cs",
        "csharp":  ".cs",
        "dotnet":  ".cs",
        "py":      ".py",
        "python":  ".py",
        "python2": ".py",
        "python3": ".py",
    }

    language_file_extension = file_extension_switcher.get(language, "unknown")

    if language_file_extension == "unknown":
        raise ValueError('Remote-compile is of unknown language "' + language + '". Please check the remote ' +
                         'compilers runtime page (' + REMOTE_COMPILE_URL + '/Api/Help/Runtimes) to see all ' +
                         'supported runtimes.')

    if 'stdin' in property_names:
        stdin = properties['stdin']

    if 'args' in property_names:
        args = properties['args']

    response = execute_code(code, stdin, args, language, language_version, language_file_extension)
    logging.debug(response)
    stdout = response["run"]["stdout"]

    return Para([Str(stdout)])


if __name__ == '__main__':
    toJSONFilter(remote_compile)
