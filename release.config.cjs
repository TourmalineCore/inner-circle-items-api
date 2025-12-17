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
        prepareCmd: 'echo ${nextRelease.version} > __version'
      }
    ],
    [
      // Analyzes commits and determines which release version should be released.
      '@semantic-release/commit-analyzer',
      {
        // https://github.com/semantic-release/commit-analyzer/issues/231#issuecomment-2127394159
        preset: "conventionalcommits",
        releaseRules: "./release.rules.cjs"
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