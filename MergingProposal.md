# Steps to Require Pull Requests for Merging

## Navigate to Your Repository:
Go to your GitHub repository where you want to enforce the pull request policy.

## Access Settings:
Click on the **Settings** tab at the top of your repository page.

## Branch Protection Rules:
In the left sidebar, click on **Branches**.  
Under the **Branch protection rules** section, click on **Add rule**.

## Specify Branches:
In the **Branch name pattern** field, enter `main` or `development`, depending on which branch you want to protect. You can also use wildcards (e.g., `*`) if needed.

## Enable Required Pull Requests:
Check the box for **Require pull request reviews before merging**. This ensures that any changes must go through a pull request and be reviewed before they can be merged.  
You can specify how many approving reviews are required before merging.

## Additional Options (Optional):
You may also want to enable other options such as:
- **Require status checks to pass before merging**: This ensures that all tests pass before merging.
- **Include administrators**: This applies the same rules to repository administrators.
- **Restrict who can push to matching branches**: This limits who can push directly to these branches, further enforcing the use of pull requests.

## Save Changes:
After configuring your branch protection rules, click on the **Create** or **Save changes** button.