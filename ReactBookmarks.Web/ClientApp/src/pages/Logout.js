import axios from 'axios';
import React, { useEffect } from 'react';
import { useHistory } from 'react-router-dom';
import { useUser } from '../AuthContext';

const Logout = () => {
    const history = useHistory();
    const {setUser} = useUser();

    useEffect(() => {
        const doLogout = async () => {
            setUser(null);
            await axios.post('/api/account/logout');
        }
        
        doLogout();
        history.push('/');

    }, []);

    return <></>
}

export default Logout;