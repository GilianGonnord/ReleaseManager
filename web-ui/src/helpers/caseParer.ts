export type CamelToSnake<T extends string> = string extends T ? string :
    T extends `${infer C0}${infer R}` ?
    `${C0 extends Lowercase<C0> ? "" : "_"}${Lowercase<C0>}${CamelToSnake<R>}` :
    "";


export type CamelKeysToSnake<T> = {
    [K in keyof T as CamelToSnake<Extract<K, string>>]: T[K]
}