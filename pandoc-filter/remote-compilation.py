
from pandocfilters import toJSONFilter
import sys
import logging

logging.basicConfig(stream=sys.stderr, level=logging.DEBUG)

"""
Pandoc filter that executes code-blocks, which 
are marked with the "remote-compile" class on a 
remote server. The programs output on stdout is 
then returned
"""


def execute_code(code, language, version):
    return


def remote_compile(key, value, format_, meta):
    if key != 'CodeBlock':
        return

    [[ident, classes, properties], code] = value
    properties = dict(properties)
    property_names = properties.keys()

    if 'remote-compile' not in classes:
        return

    if 'language' not in property_names:
        raise ValueError('Remote-compile code-block is missing the "language" property.' +
                         'Cannot determine which language the code is written in.')

    if 'language-version' not in property_names:
        raise ValueError('Remote-compile code-block is missing the "language-version" property. ' +
                         'Cannot determine which version of language "' + properties['language'] + '" ' +
                         'the code is written in.')

    logging.debug(properties)
    logging.debug(code)
    logging.debug('\n')

    language = properties['language']
    language_version = properties['language-version']
    output = execute_code(language, language_version, code)


if __name__ == '__main__':
    toJSONFilter(remote_compile)