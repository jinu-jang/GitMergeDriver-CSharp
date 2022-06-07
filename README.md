# Custom Merge Driver Example
Small example of how custom merge drivers can be built in CSharp.<br>

# How to Run
Development was done through Visual Studio, hence Visual Studio is recommended.

1. Copy the contents of `config` file to `.git/config`.
2. `git checkout SAMPLE_A`
3. `git merge SAMPLE_B`

# What's Happening?
`.gitattributes` define which merge driver to use, and the definition for the merge driver needs to be in `.git/config`.<br>
It is also possible to move the definition to the root level git configuration at `~/.gitconfig`.

The manual for custom merge drivers can be found here: https://git-scm.com/docs/gitattributes#_defining_a_custom_merge_driver.

# TODO
`git merge` is a highly configurable command. Therefore our merge driver must cover many different fronts if it plans to override the entire experience.

## Honor  Merge Strategies
From experimentation, custom merge drivers are not called when on options `-s ours` or `-s theirs`.
Seemingly impossible to honor strategy other options passed in though. Other strategies can be seen here: [link](https://git-scm.com/docs/git-merge#_merge_strategies)

Under certain merge strategies, such as recursive, it is even possible to select the diff algorithm. No currently known way to honor this flag.

## Recursive=text
The `merge.*.recursive` option defines behavior for all the "internal" merges leading up to the final merge.<br>
Leaving this option to `binary` leads to default behavior for git, but other options such as leaving it blank or using `text` needs to be tested.