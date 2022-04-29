import { Control } from 'react-hook-form'
import { GitProviders } from '../../models/AppModel'
import ControlledTextField from '../Atomes/Inputs/ControlledTextField'
import { NewAppInput } from '../Pages'

type Props = {
    gitProvider: GitProviders,
    control: Control<NewAppInput>
}

export const AppCredentialInputs = (props: Props) => {
    const { control, gitProvider } = props;

    var result;

    switch (gitProvider) {
        case GitProviders.GITHUB:
            result = (<>
                <ControlledTextField
                    defaultValue=""
                    name="githubUsername"
                    control={control}
                    label="Github username"
                    autoComplete="app-github-username"
                    required
                />
                <ControlledTextField
                    defaultValue=""
                    name="githubProject"
                    control={control}
                    label="Github project"
                    autoComplete="app-github-project"
                    required
                />
                <ControlledTextField
                    defaultValue=""
                    name="githubAccessToken"
                    control={control}
                    label="Github access token"
                    autoComplete="app-github-access-token"
                    required
                />
            </>);
            break;
        case GitProviders.GITLAB:
            result = (<>
                <ControlledTextField
                    defaultValue=""
                    name="gitlabOwner"
                    control={control}
                    label="Gitlab owner"
                    autoComplete="app-gitlab-owner"
                    required
                />
                <ControlledTextField
                    defaultValue=""
                    name="gitlabProject"
                    control={control}
                    label="Gitlab project"
                    autoComplete="app-gitlab-project"
                    required
                />
            </>);
            break;
        case GitProviders.NONE:
            result = (
                <ControlledTextField
                    defaultValue=""
                    name="repoUrl"
                    control={control}
                    label="Repository URL"
                    autoComplete="app-repo-url"
                    type="url"
                    required
                />
            );
            break;
    }

    return result;
}