import { UseQueryResult } from "react-query";
import { Loading } from "../Atomes/Loading";

type QueryStateHandlerProps<T> = {
    query: UseQueryResult<T, unknown>,
    render: (data: T) => JSX.Element,
    renderEmpty?: () => JSX.Element
}

export const QueryStateHandler = <T,>(props: QueryStateHandlerProps<T>): JSX.Element => {
    const { query, render, renderEmpty } = props;

    if (query.isError) {
        return <span>Error</span>
    }

    if (!query.isSuccess) {
        return <Loading />;
    }

    if (renderEmpty && Array.isArray(query.data) && query.data.length === 0) {
        return renderEmpty()
    }

    return render(query.data);
}
