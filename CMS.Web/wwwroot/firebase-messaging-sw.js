importScripts(
    "https://www.gstatic.com/firebasejs/13.0.0/firebase-app-compat.js");

importScripts(
    "https://www.gstatic.com/firebasejs/13.0.0/firebase-messaging-compat.js");

firebase.initializeApp({
    apiKey: "AIzaSyDYmOrv-5ANKBq5VorLdbPacRQ_u0KvwGo",
    authDomain: "cms-task22.firebaseapp.com",
    projectId: "cms-task22",
    storageBucket: "cms-task22.firebasestorage.app",
    messagingSenderId: "144044704529",
    appId: "1:144044704529:web:733ced0efc0696824d02b0"
});

const messaging = firebase.messaging();

messaging.onBackgroundMessage((payload) => {
    const title = payload.data?.Title ?? "CMS";

    const options = {
        body: payload.data?.Body ?? ""
    };

    self.registration.showNotification(title, options);
});