export enum GitProviders {
    NONE = 0,
    GITHUB,
    GITLAB
};

export class App {
    id?: number;
    name: string;
    gitProvider: GitProviders;
    githubUsername?: string;
    githubProject?: string;
    githubAccessToken?: string;
    gitlabOwner?: string;
    gitlabProject?: string;
    repoUrl?: string;

    private constructor(name: string, repoUrl: string, gitProvider: GitProviders) {
        this.name = name;
        this.repoUrl = repoUrl;
        this.gitProvider = gitProvider;
    }
}