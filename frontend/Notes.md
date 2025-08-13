# Frontend for Vehicle Marketplace

- [Cookie](#cookie)
- [](#)

<hr/>

## Cookie

Cookie is automatically saved in browser from backend, but for
any request from frontend you need to add: `withCredentials: true`

Example:

```ts
public testAuthorization() {

    return this.httpClient.get(`${this.url}/test`, { withCredentials: true });
}
```

## 