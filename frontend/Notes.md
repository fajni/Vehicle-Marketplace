# Frontend for Vehicle Marketplace

- [Cookie](#cookie)
- [Some problems/solutions](#some-problemssolutions)

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

## Some problems/solutions

PROBLEM: subscription inside subscription - __anti-pattern__

- Problem:

```ts
public ngOnInit(): void {

    const subscription1 = this.loginService.testAuthorization().subscribe({
        next: (response) => {

            this.account = response;

            const subscription2 = this.carService.getUserCarsByUserId(this.account.id).subscribe({
                next: (response) => { this.cars = response; },
                error: (error) => { console.log(error); }
            });

            this.destroyRef.onDestroy(() => { subscription2.unsubscribe(); });

        },
        error: (error) => { console.log(error); }
    });

    this.destroyRef.onDestroy(() => { subscription.unsubscribe(); });
}
```

- Solution (_switchMap_):

```ts
public ngOnInit(): void {

    const subscription = this.loginService.testAuthorization().pipe(
        switchMap(response => this.accountService.getUserByEmail(response.userEmail!).pipe(
            map(account => ({...response, account}))
        )),
        switchMap(response => this.carService.getUserCarsByUserId(response.account!.id).pipe(
            map(cars => ({ ...response, cars }))
        ))
    ).subscribe({
        next: (response) => {
            // response will get new fields
            this.account = response.account;
            this.myCars = response.cars; 
        },
        error: (error) => { console.log(error); }
    });

    this.destroyRef.onDestroy(() => { subscription.unsubscribe(); });

}
```