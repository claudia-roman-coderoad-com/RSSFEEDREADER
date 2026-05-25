#!/usr/bin/env bash
# Helper to create a feature branch and commit changes locally
set -euo pipefail
BRANCH=${1:-mvp-rss-reader}

git checkout -b "$BRANCH"
git add .
git commit -m "feat(mvp): implement MVP subscription management scaffold and tests"

echo "Created branch $BRANCH and committed changes. Push and open a PR from your Git provider." 
