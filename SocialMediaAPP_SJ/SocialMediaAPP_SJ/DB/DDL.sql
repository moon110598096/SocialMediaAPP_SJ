create table user
(
    user_id  varchar(255) null,
    password text         null,
    name     text         null,
    constraint user_pk
        primary key (user_id)
);

create table post
(
    title   text         null,
    content text         null,
    date    date         null,
    post_id varchar(255) null,
    user_id varchar(255) null,
    removed bool         null,
    constraint post_pk
        primary key (post_id),
    constraint post_user_user_id_fk
        foreign key (user_id) references user (user_id)
);

create table comment
(
    comment_id varchar(255) null,
    name       text         null,
    message    text         null,
    ip         text         null,
    date       date         null,
    removed    bool      null,
    user_id    varchar(255) null,
    post_id    varchar(255) null,
    constraint comment_pk
        primary key (comment_id),
    constraint comment_post_user_id_post_id_fk
        foreign key (user_id, post_id) references post (user_id, post_id)
);