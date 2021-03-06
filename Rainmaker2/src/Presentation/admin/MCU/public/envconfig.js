window.envConfig = {
   API_BASE_URL: 'https://qamaingateway.rainsoftfn.com',
   //API_BASE_URL: "https://devMainGateway.rainsoftfn.com:5001",
   RAIN_MAKER_URL: "https://localdev.rainsoftfn.com",
   IDLE_TIMER: '10', // Must be in minutes
   LOCK_RETAIN_DURATION: 30, // In seconds
   // We are using trial/evaluation version of PSPDF Kit.
   PSPDF_LICENSE_KEY: 'Xq2sbPLKcoMngmloCFRhq1HUgk0jQLLbOf6LosAo6oO8y2G9QoaX3w3aX0PWavM6WOVdQo49a7UbnVe1GG6vkS1oSYDJv4EsuCckA4sx6M1qwqn9NbaszHkR6dvE8F0UhxZUsvIIRKUQQ67XwqwCd5G5iBfiJG6NE6gZRu-zasYtvEoyQ1uufbWcWF6FoXV6P_1FOcHrXqToVEXUqYVdYoPtXT3o_gEhLIp3mkLmIWXA2sUuMYZKKAHie1Wqu1eD1mpL0EzxadBtTVAPjLL8xMgl3h0PRZppCtQswQVFCQQMYwMLmDXG7Mzc_v8SO7z_3-CpjubR71MiAaMiw-jRCS8NfVnpso5pCws5gB3uhgxb4x94ISus4h1I0kiN9n7rsihbeJwn16L0-wuxDhuRr-Yyhh2WYdcQz-BfX6XXTTEThzESMHyrWWSJ6KNSNJLq',
   // We need to provide base url for PSPDFKIT
   PSPDF_LICENSE_BASE_URL: `${window.location.protocol}//${window.location.host}/DocumentManagement/`
};
