export function isSignedIn(){
    const token = JSON.parse(localStorage.getItem('Token'))

    if(!token){
        return false;
    }

    try {
        const isExpired = Date.now() > token.token.expiration ;

        if(isExpired){
            return false;
        }

        return true;
    } catch (error) {
        return false;
    }
}

export function MenuAtivo(){
    
}