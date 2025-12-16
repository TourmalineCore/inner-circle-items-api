module.exports = {
  // You can find out more about the configuration of this file here https://semantic-release.gitbook.io/semantic-release/usage/configuration
  branches: [
    // Replace with the master before merging into it
    'feature/add-semantic-release'
  ],
  // Plugins https://semantic-release.gitbook.io/semantic-release/extending/plugins-list
  plugins: [
    [
      '@semantic-release/exec',
      {
        // Need to rewrite version in __version
        successCmd: 'echo ${nextRelease.version} > __version'
      }
    ],
    [
      // Analyzes commits and determines which release version should be released.
      '@semantic-release/commit-analyzer',
      {
        // Disables standard presets
        preset: false,
        parserOpts: {
          // It is necessary that the types with ! you were right to worry,
          // without this feat!, refactor! and fix! did not update the major version
          headerPattern: /^(\w+!?): (.+)$/
        },
        // Custom rules
        releaseRules: [
          {
            type: 'feat!',
            release: 'major'
          },
          {
            type: 'feat',
            release: 'minor'
          },
          {
            type: 'fix!',
            release: 'major'
          },
          {
            type: 'fix',
            release: 'patch'
          },
          {
            type: 'refactor!',
            release: 'major'
          },
          {
            type: 'refactor',
            release: 'patch'
          },
          {
            type: 'format',
            release: 'patch'
          },
          {
            type: 'cd',
            release: 'patch'
          },
          {
            type: 'docs',
            release: null
          },
          {
            type: 'infra',
            release: null
          },
          {
            type: 'ci',
            release: null
          },
          {
            type: 'git',
            release: null
          }
        ]
      }
    ],
    // Add release notes
    [
      '@semantic-release/release-notes-generator',
      {
        writerOpts: {
          commitsSort: ['scope', 'subject']
        }
      }
    ],
    // Plugin for creating releases
    '@semantic-release/github',
    [
      // Plugin for commits changes
      '@semantic-release/git',
      {
        // Tracks the __version file in the project root
        // And updates this file with a new version with each release
        // Also add this file to commit
        assets: ['__version'],
        // Release commit message
        message: 'chore(release): ${nextRelease.version}'
      }
    ]
  ],
  tagFormat: '${version}'
}