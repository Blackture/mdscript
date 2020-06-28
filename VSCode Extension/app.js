vscode.languages.registerHoverProvider('mdscript', {
    provideHover(document, position, token) {
      return {
        contents: ['Hover Content']
      };
    }
  });
